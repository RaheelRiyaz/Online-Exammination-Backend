using AutoMapper;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;
using System.Net;

namespace OnlineExammination.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository repository;
        private readonly IMapper mapper;

        public NotificationService(INotificationRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<NotificationResponse>> AddNotification(NotificationRequest model)
        {
            var notification = this.mapper.Map<Notification>(model);
            var res = await repository.Add(notification);

            if (res > 0)
            {
                return new ApiResponse<NotificationResponse>()
                {
                    IsSuccess = true,
                    Message = "Notification Added",
                    Result = mapper.Map<NotificationResponse>(notification),
                    StatusCode = HttpStatusCode.Created
                };
            }


            return new ApiResponse<NotificationResponse>()
            {
                Message = "There is some issue please try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public async Task<ApiResponse<NotificationResponse>> DeleteNotification(Guid id)
        {
            var notification = await repository.GetById(id);

            if(notification  is not null)
            {
                var res = await repository.Delete(notification);   
                if(res > 0)
                {
                    return new ApiResponse<NotificationResponse>()
                    {
                        IsSuccess=true,
                        Message="Notification Deleted Successfully",
                        Result=mapper.Map<NotificationResponse>(notification),
                        StatusCode=HttpStatusCode.OK
                    };
                }
            }


            return new ApiResponse<NotificationResponse>()
            {
                Message = "No Notification Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }



        public async Task<ApiResponse<NotificationResponse>> DeleteNotifications(List<Guid> ids)
        {
            var res=await repository.DeleteRange(ids);
            if (res > 0)
            {
                return new ApiResponse<NotificationResponse>()
                {
                    IsSuccess = true,
                    Message = $"{ ids.Count()} Notifications Deleted Successfully",
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<NotificationResponse>()
            {
                Message = "There is some issue please try again later",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }



        public async Task<ApiResponse<NotificationResponse>> GetLatestNotification()
        {
            var notifications = await repository.GetAll();
            if (notifications.Any())
            {
                var latestNotification = notifications.OrderByDescending(_ => _.CeatedOn).First();
                return new ApiResponse<NotificationResponse>()
                {
                    IsSuccess = true,
                    Message = "Latest Notification",
                    Result = mapper.Map<NotificationResponse>(latestNotification),
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResponse<NotificationResponse>()
            {
                Message = "No Notification Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public async Task<ApiResponse<NotificationResponse>> GetNotificationById(Guid id)
        {
            var notification = await repository.GetById(id);
            if (notification is not null)
            {
                return new ApiResponse<NotificationResponse>()
                {
                    IsSuccess = true,
                    Message = "Notification Added",
                    Result = mapper.Map<NotificationResponse>(notification),
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<NotificationResponse>()
            {
                Message = "No Notification Found",
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public async Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotifications()
        {
            var notifications = await repository.GetAll();
            if (notifications.Any())
            {
                return new ApiResponse<IEnumerable<NotificationResponse>>()
                {
                    IsSuccess = true,
                    Message = "Success",
                    Result = notifications.Select(_ => new NotificationResponse()
                    {
                        Description = _.Description,
                        Id = _.Id
                    }),
                    StatusCode = HttpStatusCode.OK
                };
            }
            return new ApiResponse<IEnumerable<NotificationResponse>>()
            {
                Message = "No notifications found",
                StatusCode = HttpStatusCode.NotFound
            };
        }
    }
}
