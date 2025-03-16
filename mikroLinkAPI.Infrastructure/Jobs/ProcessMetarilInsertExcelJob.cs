using GenericRepository;
using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc.Rendering;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.Rules;
using mikroLinkAPI.Domain.Validators;
using mikroLinkAPI.Infrastructure.Repositories;
using OfficeOpenXml;


namespace mikroLinkAPI.Infrastructure.Jobs
{

    public class ProcessMetarilInsertExcelJob(IFileRecordRepository fileRecordRepository,
                                IComponentRepository componentRepository,
                                IMetarialRepository metarialRepository,
                                IUnitOfWork unitOfWork,
                                IExcelHelper excelHelper,
                                IExcelConvert excelConvert) : IProcessMetarilInsertExcelJob
    {
        [AutomaticRetry(Attempts = 1)]
        public async Task RunAsync(int fileId, int userId, int companyId, PerformContext context)
        {
            var jobId = context.BackgroundJob?.Id;
            var serialCounts = new Dictionary<string, int>();
            var errors = new List<HataModel>();
            var filePath = await GetFilePathAsync(fileId);
            var records = ReadExcelFile(filePath, serialCounts, errors);
            int count = 0;
            foreach (var record in records)
            {
                var validationErrors = await ValidateRecordAsync(record, serialCounts);
                if (validationErrors.Any())
                    errors.AddRange(CreateErrorModels(record, validationErrors));
                else
                {
                    count++;
                    await SaveValidRecordAsync(record, companyId, userId);
                }
            }

            await unitOfWork.SaveChangesAsync();
            using (var connection = JobStorage.Current.GetConnection())
                connection.SetJobParameter(jobId, "TotalCount", count.ToString());
            if (errors.Any() && jobId != null)
            {
                await GenerateErrorReportAsync(errors, jobId, userId);
            }
        }

        private async Task<string> GetFilePathAsync(int fileId)
        {
            var fileRecord = await fileRecordRepository.GetFirstAsync(c => c.Id == fileId);
            return fileRecord?.FilePath ?? throw new FileNotFoundException("Dosya bulunamadı.");
        }

        private List<ComponentSerial> ReadExcelFile(string filePath, Dictionary<string, int> serialCounts, List<HataModel> errors)
        {
            var materalType = Enum.GetValues(typeof(MaterialTypeList)).Cast<MaterialTypeList>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            var records = new List<ComponentSerial>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var materialTypeString = worksheet.Cells[row, 8].Text;
                    if (!Enum.TryParse(materialTypeString, out MaterialTypeList materialType))
                    {
                        errors.Add(new HataModel
                        {
                            HataMesaji = $"Geçersiz Malzeme Türü: {materialTypeString}",
                            Serinumarasi = worksheet.Cells[row, 2].Text,
                            ComponentId = worksheet.Cells[row, 1].Text,
                            MalzemeTuru = materialTypeString
                        });
                        continue;
                    }

                    var record = new ComponentSerial
                    {
                        ComponentId = worksheet.Cells[row, 1].Text,
                        SeriNo = worksheet.Cells[row, 2].Text,
                        GIrsaliyeNo = worksheet.Cells[row, 3].Text,
                        Sturdy = int.Parse(worksheet.Cells[row, 4].Text),
                        Defective = int.Parse(worksheet.Cells[row, 5].Text),
                        Scrap = int.Parse(worksheet.Cells[row, 6].Text),
                        Shelf = worksheet.Cells[row, 7].Text,
                        MaterialType = Convert.ToInt32(materalType.FirstOrDefault(p => p.Text == worksheet.Cells[row, 8].Text).Value)
                    };

                    if (serialCounts.ContainsKey(record.SeriNo))
                        serialCounts[record.SeriNo]++;
                    else
                        serialCounts[record.SeriNo] = 1;

                    records.Add(record);
                }
            }

            return records;
        }

        private async Task<List<string>> ValidateRecordAsync(ComponentSerial record, Dictionary<string, int> serialCounts)
        {
            var component =await componentRepository.FirstOrDefaultAsync(c => c.Id == record.ComponentId);
            var validator = new MaterialValidator()
                .AddRule(new RequiredFieldRule(record.ComponentId, "Parça Kodu"))
                .AddRule(new ComponentKontrolRule(record.ComponentId,component))
                .AddRule(new RequiredFieldRule(record.SeriNo, "Seri Numarası"))
                .AddRule(new RequiredFieldRule(record.GIrsaliyeNo, "Giriş İrsaliye No"))
                .AddRule(new RequiredFieldRule(record.Shelf, "Raf"))
                .AddRule(new RequiredFieldRule(record.MaterialType.ToString(), "Malzeme Türü"))
                .AddRule(new UniqueSerialNumberRule(record.SeriNo, serialCounts))
                .AddRule(new ComponentSerialExistsRule(record.SeriNo, metarialRepository))
                .AddRule(new SarfMalzemeKontrolRule(record.ComponentId, record.SeriNo, component))
                .AddRule(new NegatifDeğerKontrolRule(record.Sturdy, record.Defective, record.Scrap))
                .AddRule(new SeriMalzemeKontrolRule(record.ComponentId, record.Sturdy, record.Defective, record.Scrap, component));

            var (isValid, errors) = await validator.ValidateAsync();
            return isValid ? new List<string>() : errors;
        }

        private List<HataModel> CreateErrorModels(ComponentSerial record, List<string> validationErrors)
        {
            return validationErrors.Select(error => new HataModel
            {
                HataMesaji = error,
                Serinumarasi = record.SeriNo,
                ComponentId = record.ComponentId,
                MalzemeTuru = record.MaterialType.ToString()
            }).ToList();
        }

        private async Task SaveValidRecordAsync(ComponentSerial record, int companyId, int userId)
        {
            var cs = new ComponentSerial
            {
                CompanyId = companyId,
                ComponentId = record.ComponentId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                Defective = record.Defective,
                GIrsaliyeNo = record.GIrsaliyeNo,
                MaterialType = record.MaterialType,
                Scrap = record.Scrap,
                SeriNo = record.SeriNo,
                Shelf = record.Shelf,
                SiteId = null,
                State = (int)State.Excel,
                Sturdy = record.Sturdy,
                TeamLeaderId = null
            };

            await metarialRepository.AddAsync(cs);
        }

        private async Task GenerateErrorReportAsync(List<HataModel> errors, string jobId, int userId)
        {
            var reportBytes = excelConvert.ModelToExcel(errors);
            var filePath = $"{jobId}.xlsx";
            var fileId = await excelHelper.GenerateFilePathByPathAsync(filePath, reportBytes, userId);
            using (var connection = JobStorage.Current.GetConnection())
                connection.SetJobParameter(jobId, "ErrorReportFileId", fileId.ToString());
        }


    }
}
