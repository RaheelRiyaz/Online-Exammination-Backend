using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(OnlineExamminationDbContext context):base(context)
        {
        }
    }
}
