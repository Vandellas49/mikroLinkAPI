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
using OfficeOpenXml;

namespace mikroLinkAPI.Infrastructure.Jobs
{
    public class ProcessComponentInsertExcelJob(
        IComponentRepository componentRepository,
        IFileRecordRepository fileRecordRepository,
        IExcelConvert excelConvert,
        IExcelHelper excelHelper,
        IUnitOfWork unitOfWork) : IProcessComponentInsertExcelJob
    {
        [AutomaticRetry(Attempts = 0)]
        public async Task RunAsync(int fileId, int userId, PerformContext context)
        {
            var jobId = context.BackgroundJob?.Id;
            var componentCounts = new Dictionary<string, int>();
            var errors = new List<HataModel>();

            var filePath = await GetFilePathAsync(fileId);
            var records = ReadExcelFile(filePath, componentCounts, errors);
            int count = 0;
            foreach (var record in records)
            {
                var validationErrors = await ValidateRecordAsync(record, componentCounts);
                if (validationErrors.Any())
                    errors.AddRange(CreateErrorModels(record, validationErrors));
                else
                {
                    count++;
                    await SaveValidRecordAsync(record, userId);
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

        private List<Component> ReadExcelFile(string filePath, Dictionary<string, int> componentCounts, List<HataModel> errors)
        {
            var materalType = Enum.GetValues(typeof(MalzemeTuru)).Cast<MalzemeTuru>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            var records = new List<Component>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                if (worksheet.Cells.Count() != 3)
                    throw new Exception("Excel geçersiz.Excelde parça kodu,ekipman açıklaması ve malzeme türü alanları olmalıdır.");
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var materialTurString = worksheet.Cells[row, 3].Text;
                    if (!Enum.TryParse(materialTurString, out MalzemeTuru materialTur))
                    {
                        errors.Add(new HataModel
                        {
                            HataMesaji = $"Geçersiz Malzeme Türü: {materialTurString}",
                            ComponentId = worksheet.Cells[row, 1].Text,
                            MalzemeTuru = materialTurString
                        });
                        continue;
                    }

                    var record = new Component
                    {
                        Id = worksheet.Cells[row, 1].Text,
                        EquipmentDescription = worksheet.Cells[row, 2].Text,
                        MalzemeTuru = Convert.ToInt32(materalType.FirstOrDefault(p => p.Text == worksheet.Cells[row, 3].Text).Value)
                    };

                    if (componentCounts.ContainsKey(record.Id))
                        componentCounts[record.Id]++;
                    else
                        componentCounts[record.Id] = 1;

                    records.Add(record);
                }
            }

            return records;
        }

        private async Task<List<string>> ValidateRecordAsync(Component record, Dictionary<string, int> componentCounts)
        {
            var component = await componentRepository.FirstOrDefaultAsync(c => c.Id == record.Id);
            var validator = new MaterialValidator()
                .AddRule(new RequiredFieldRule(record.Id, "Parça Kodu"))
                .AddRule(new ComponentExistsRule(record.Id, componentRepository))
                .AddRule(new RequiredFieldRule(record.EquipmentDescription, "Ekipman Açıklaması"))
                .AddRule(new MalzemeTuruKontrolRule(record.MalzemeTuru))
                .AddRule(new UniqueComponentIdRule(record.Id, componentCounts));
            var (isValid, errors) = await validator.ValidateAsync();
            return isValid ? new List<string>() : errors;
        }

        private List<HataModel> CreateErrorModels(Component record, List<string> validationErrors)
        {
            return validationErrors.Select(error => new HataModel
            {
                HataMesaji = error,
                ComponentId = record.Id,
                MalzemeTuru = record.MalzemeTuru.ToString()
            }).ToList();
        }

        private async Task SaveValidRecordAsync(Component record, int userId)
        {
            var cs = new Component
            {
                EquipmentDescription = record.EquipmentDescription,
                Id = record.Id,
                MalzemeTuru = record.MalzemeTuru
            };

            await componentRepository.AddAsync(cs);
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
