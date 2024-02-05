using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class NotificationRepository:BaseRepository<Notification>,INotificationRepository
    {
        public NotificationRepository(OnlineExamminationDbContext context):base(context){}
    }
}
