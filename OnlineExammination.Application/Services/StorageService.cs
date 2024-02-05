using Microsoft.AspNetCore.Http;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string webRootPath;

        public StorageService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }
        public async Task DeleteFileAsync(string filePath)
        {
            await Task.Run(() => File.Delete(webRootPath + filePath));
        }

        public async Task DeleteFilesAsync(List<string> filePaths)
        {
            await Task.Run(() =>
            {
                foreach (var filePath in filePaths)
                {
                    File.Delete(webRootPath + filePath);
                }
            });
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string filePath = String.Concat(Guid.NewGuid(), file.FileName);
            string applicationFilePath = string.Concat(GetPhysicalFileAddress(), filePath);

            using FileStream stream = new FileStream(applicationFilePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return GetVirtualFileAddress() + filePath;
        }

        public async Task<List<string>> SaveFilesAsync(IFormFileCollection files)
        {
            List<string> filePaths = new List<string>();

            foreach (var file in files)
            {
                string filePath = string.Concat(Guid.NewGuid(), file.FileName);
                string applicationFilePath = string.Concat(GetPhysicalFileAddress(), filePath);

                FileStream fileStream = new FileStream(applicationFilePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                filePaths.Add(GetVirtualFileAddress() + filePath);
            }

            return filePaths;
        }

        private string GetVirtualFileAddress() => "/Files/";
        private string GetPhysicalFileAddress() => string.Concat(this.webRootPath, GetVirtualFileAddress());

    }
}
