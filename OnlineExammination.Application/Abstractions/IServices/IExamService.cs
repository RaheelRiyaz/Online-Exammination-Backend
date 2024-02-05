using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface IExamService
    {
        Task<ApiResponse<IEnumerable<ExamResponse>>> GetCompactExams();
        Task<ApiResponse<IEnumerable<ExamResponse>>> SearchExams(string term);
        Task<ApiResponse<ExamResponse>> GetCompactExamById(Guid id);
        Task<ApiResponse<Exam>> AddNewExam(ExamRequest model);
        Task<ApiResponse<ExamResponse>> UpdateExam(UpdateExamRequest model);
    }
}
