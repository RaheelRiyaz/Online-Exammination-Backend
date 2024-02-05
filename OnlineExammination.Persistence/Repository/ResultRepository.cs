using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class ResultRepository:BaseRepository<Result>,IResultRepository
    {
        public ResultRepository(OnlineExamminationDbContext context):base(context) { }


        public async Task<Result?> GetResultByExamId(Guid examId)
        {
            string query = $"SELECT * FROM Results WHERE ExamId=@examId";
            return await context.FirstOrDefaulyAsync<Result>(query, new { examId });
        }


        public async Task<int> UploadResult(Guid examId)
        {
            string query = $@"UPDATE Results SET IsReleased = 1 WHERE ExamId = @examId";
            return await context.ExecuteAsync(query, new { examId });
        }
    }
}
