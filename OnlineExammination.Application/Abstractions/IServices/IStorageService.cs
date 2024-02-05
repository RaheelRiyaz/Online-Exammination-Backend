using Microsoft.AspNetCore.Http;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);

        Task<List<string>> SaveFilesAsync(IFormFileCollection files);
        Task DeleteFileAsync(string filePath);
        Task DeleteFilesAsync(List<string> filePaths);
    }
}
