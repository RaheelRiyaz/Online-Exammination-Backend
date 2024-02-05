using Microsoft.AspNetCore.Http;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Application.RRModels
{
    public class AppFileRequst
    {
        public Guid EntityId { get; set; }
        public IFormFile File { get; set; } = null!;
        public Module Module { get; set; }
    }

    public class AppFileResponse:AppFile
    {

    }

    public class GalleryResponse
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }


    public class GalleryRequest
    {
        public IFormFileCollection Files { get; set; } = null!;
    }
}
