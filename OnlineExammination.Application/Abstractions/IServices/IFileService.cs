using Microsoft.AspNetCore.Http;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface IFileService
    {
        Task<ApiResponse<AppFileResponse>> UploadFileAsync(AppFileRequst model);

        Task<AppFile?> GetFilePathByEntityId(Guid entityId);

        Task<ApiResponse<IEnumerable<GalleryResponse>>> GetAllImages();
        Task<ApiResponse<IEnumerable<GalleryResponse>>> SaveGalleryImages(IFormFileCollection files);
        Task<ApiResponse<GalleryResponse>> DeleteImage(Guid id);
        Task<ApiResponse<IEnumerable<GalleryResponse>>> DeleteImages(List<Guid> ids);
    }
}
