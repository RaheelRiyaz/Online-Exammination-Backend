using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IRepository
{
    public interface IResultRepository:IBaseRepository<Result>
    {
        Task<Result?> GetResultByExamId(Guid examId);
        Task<int> UploadResult(Guid examId);
    }
}
