using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService service;

        public NotificationsController(INotificationService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotifications()
        {
            return await this.service.GetNotifications();
        }


        [HttpGet("{id:guid}")]
        public async Task<ApiResponse<NotificationResponse>> GetNotificationById(Guid id)
        {
            return await this.service.GetNotificationById(id);
        }


        [HttpPost]
        public async Task<ApiResponse<NotificationResponse>> AddNotification(NotificationRequest model)
        {
            return await this.service.AddNotification(model);
        }


        [HttpGet("latest-notification")]
        public async Task<ApiResponse<NotificationResponse>> GetLatestNotification()
        {
            return await this.service.GetLatestNotification();
        }


        [HttpDelete("{id:guid}")]
        public async Task<ApiResponse<NotificationResponse>> Delete(Guid id)
        {
            return await this.service.DeleteNotification(id);
        } 
        
        
        [HttpPost("delete-notifications")]
        public async Task<ApiResponse<NotificationResponse>> Delete(List<Guid> ids)
        {
            return await this.service.DeleteNotifications(ids);
        }
    }
}
