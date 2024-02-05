using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IFileService fileService;

        public GalleryController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> GetAllImages()
        {
            return await fileService.GetAllImages();
        }



        [HttpPost]
        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> AddGalleryImages(IFormFileCollection files)
        {
            return await fileService.SaveGalleryImages(files);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ApiResponse<GalleryResponse>> DeleteImage(Guid id)
        {
            return await fileService.DeleteImage(id);
        }


        [HttpPost("deleteimages")]
        public async Task<ApiResponse<IEnumerable<GalleryResponse>>> DeleteImages(List<Guid> ids)
        {
            return await fileService.DeleteImages(ids);
        }
    }
}
