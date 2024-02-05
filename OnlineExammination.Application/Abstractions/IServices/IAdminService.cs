using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface IAdminService
    {
        Task<ApiResponse<SemesterResponse>> AddSemster(SemesterRequest model);
        Task<ApiResponse<PaperResponse>> AddPaper(PaperRequest model);
        Task<ApiResponse<ProgramResponse>> AddProgram(ProgramRequest model);
        Task<ApiResponse<IEnumerable<ProgramResponse>>> GetProgramList();
        Task<ApiResponse<IEnumerable<ProgramResponse>>> SearchPrograms(string term);
        Task<ApiResponse<IEnumerable<SemesterResponse>>> GetSemesterList();
        Task<ApiResponse<IEnumerable<SemesterResponse>>> SearchSemesters(int sem);
        Task<ApiResponse<IEnumerable<PaperResponse>>> GetQuestionPaperByExamId(Guid id);
        Task<ApiResponse<StudentPaperResponse>> GetQuestionPaperForStudentByExamId(Guid id);
        Task<ApiResponse<int>> ConductExam(Guid examId);
        Task<ApiResponse<int>> SubmitStudentResult(SubmitPaper model);
        Task<ApiResponse<IEnumerable<StudentResult>>> GetStudentResultByRegNumber(ResultRequest model);
        Task<ApiResponse<PreviousPaperResponse>> GetAllPreviousPapers(Guid examId, Guid semesterId);
        Task<ApiResponse<int>> UploadResult(UploadResultResquest model);

    }
}
