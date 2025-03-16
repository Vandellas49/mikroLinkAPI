using GenericRepository;
using Microsoft.AspNetCore.Http;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;

namespace mikroLinkAPI.Infrastructure.Services
{
    public class ExcelHelper(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IFileRecordRepository fileRecordRepository) : IExcelHelper
    {
        readonly ICurrentUserService _currentUserService = currentUserService;
        readonly IFileRecordRepository _fileRecordRepository = fileRecordRepository;
        readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        public async Task<int> GenerateFilePathAsync(IFormFile file)
        {
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var fileRecord = new FileRecord
            {
                FileName = fileName,
                OriginalName = file.FileName,
                FilePath = filePath,
                Status = "Uploaded",
                CreatedBy = _currentUserService.UserId,
                CreatedDate = DateTime.Now
            };
            await _fileRecordRepository.AddAsync(fileRecord);
            await _unitOfWork.SaveChangesAsync();
            return fileRecord.Id;

        }
        public async Task<int> GenerateFilePathByPathAsync(string path, byte[] reportBytes,int userId)
        {
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(path);
            var filePath = Path.Combine(_uploadFolder, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, reportBytes);
            var fileRecord = new FileRecord
            {
                FileName = fileName,
                OriginalName = fileName,
                FilePath = filePath,
                Status = "HataLog",
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };
            await _fileRecordRepository.AddAsync(fileRecord);
            await _unitOfWork.SaveChangesAsync();
            return fileRecord.Id;

        }
    }
}