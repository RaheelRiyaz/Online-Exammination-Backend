using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface INotificationService
    {
        Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotifications();
        Task<ApiResponse<NotificationResponse>> GetNotificationById(Guid id);
        Task<ApiResponse<NotificationResponse>> DeleteNotification(Guid id);
        Task<ApiResponse<NotificationResponse>> DeleteNotifications(List<Guid> ids);
        Task<ApiResponse<NotificationResponse>> GetLatestNotification();
        Task<ApiResponse<NotificationResponse>> AddNotification(NotificationRequest model);
    }
}
