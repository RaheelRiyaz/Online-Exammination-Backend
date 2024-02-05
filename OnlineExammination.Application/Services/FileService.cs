using AutoMapper;
using Microsoft.AspNetCore.Http;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Application.Utilis;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Domain.Enums;
using System.Net;


namespace OnlineExammination.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IStorageService storageService;
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;

        public FileService(IStorageService storageService, IFileRepository fileRepository, IMapper mapper)
        {
            this.storageService = storageService;
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }


        public async Task<ApiResponse<AppFileResponse>> UploadFileAsync(AppFileRequst model)
        {
            model.File.IsImageFormat();

            var entityFile = mapper.Map<AppFile>(model);
            var isProfileUploaded = await fileRepository.GetFilePathByEntityId(model.EntityId);
            if (isProfileUploaded is not null)
            {
                await fileRepository.Delete(isProfileUploaded.Id);
                await storageService.DeleteFileAsync(isProfileUploaded.FilePath);
            }

            string filePath = await storageService.SaveFileAsync(model.File);
            entityFile.FilePath = filePath;

            var res = await fileRepository.Add(entityFile);
            if (res > 0)
            {
                return new ApiResponse<AppFileResponse>()
                {
                    IsSuccess = true,
                    Message = "File Uploaded Successfully",
                    Result = mapper.Map<AppFileResponse>(entityFile),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<AppFileResponse>()
            {
                Message = "There is some error",
                StatusCode = HttpStatusCode.InternalServerError
            };

        }



        public async Task<AppFile?> GetFilePathByEntityId(Guid entityId)
        {
            return await fileRepository.GetFilePathByEntityId(entityId);
        }



        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> GetAllImages()
        {
            var images = await fileRepository.GetAllImages();
            if (images.Any())
            {
                return new ApiResponse<IEnumerable<GalleryResponse>>()
                {
                    IsSuccess = true,
                    Message = $"Found {images.Count()} images",
                    Result = mapper.Map<IEnumerable<GalleryResponse>>(images),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<IEnumerable<GalleryResponse>>()
            {
                Message = "No image found",
                StatusCode = HttpStatusCode.NotFound
            };
        }



        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> SaveGalleryImages(IFormFileCollection files)
        {
            files.IsImageFormat();

            var applicationFiles = await storageService.SaveFilesAsync(files);
            List<AppFile> appFiles = new List<AppFile>();

            foreach (var file in applicationFiles)
            {
                appFiles.Add(new AppFile()
                {
                    FilePath = file,
                    Module = Module.Gallery,
                    EntityId = Guid.Empty
                });
            }

            var res = await fileRepository.InsertRange(appFiles);
            if (res > 0)
            {
                return new ApiResponse<IEnumerable<GalleryResponse>>()
                {
                    IsSuccess = true,
                    Message = $"{res} files saved",
                    Result = mapper.Map<IEnumerable<GalleryResponse>>(appFiles),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<IEnumerable<GalleryResponse>>()
            {
                Message = "There is some issue please try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }


        public async Task<ApiResponse<GalleryResponse>> DeleteImage(Guid id)
        {
            var image = await fileRepository.GetById(id);

            if (image is not null)
            {
                await storageService.DeleteFileAsync(image.FilePath);
                var res = await fileRepository.Delete(image);
                if (res > 0)
                {
                    return new ApiResponse<GalleryResponse>()
                    {
                        IsSuccess = true,
                        Message = "File Deleted Successfully",
                        Result = mapper.Map<GalleryResponse>(image),
                        StatusCode = HttpStatusCode.OK
                    };
                }

                return new ApiResponse<GalleryResponse>()
                {
                    Message = "There is some issue please try again later",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new ApiResponse<GalleryResponse>()
            {
                Message = "No such file found",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        
        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> DeleteImages(List<Guid> ids)
        {
            List<AppFile> files = new List<AppFile>();
            List<string> filePaths = new List<string>();

            foreach (var id in ids)
            {
                var file = await fileRepository.GetById(id);
                if (file is not null)
                {
                    files.Add(file);
                    filePaths.Add(file.FilePath);
                };
            }

            await storageService.DeleteFilesAsync(filePaths);

            var res = await fileRepository.DeleteRange(files);

            if (res > 0)
            {
                return new ApiResponse<IEnumerable<GalleryResponse>>()
                {
                    IsSuccess = true,
                    Message = $"{res} files deleted Successfully",
                    Result = mapper.Map<IEnumerable<GalleryResponse>>(files),
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<IEnumerable<GalleryResponse>>()
            {
                Message = "There is some isue please try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
