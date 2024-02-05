using OnlineExammination.Application.RRModels;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IRepository
{
    public interface IExamRepository:IBaseRepository<Exam>
    {
        Task<IEnumerable<ExamResponse>> GetCompactExams();
        Task<ExamResponse?> GetCompactExamById(Guid id);
    }
}
