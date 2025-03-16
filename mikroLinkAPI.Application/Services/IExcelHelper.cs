using Microsoft.AspNetCore.Http;
namespace mikroLinkAPI.Application.Services
{
    public interface IExcelHelper
    {
        Task<int> GenerateFilePathAsync(IFormFile file);
        Task<int> GenerateFilePathByPathAsync(string path, byte[] reportBytes, int userId);
    }
}
