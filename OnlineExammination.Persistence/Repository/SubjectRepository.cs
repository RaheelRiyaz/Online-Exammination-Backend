using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class SubjectRepository:BaseRepository<Subject>,ISubjectRepository
    {
        public SubjectRepository(OnlineExamminationDbContext context):base(context){}
    }
}
