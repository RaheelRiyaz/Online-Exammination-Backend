using OnlineExammination.Application.RRModels;
using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.Abstractions.IRepository
{
    public interface IAdmin:IBaseRepository<User>
    {
        Task<int> AddPaper(QuestionPaper model);
        Task<int> AddProgram(Program model);
        Task<int> AddSemester(Semester model);
        Task<IEnumerable<Program>> GetPrograms();
        Task<IEnumerable<ProgramResponse>> SearchPrograms(string term);
        Task<IEnumerable<Semester>> GetSemesters();
        Task<IEnumerable<SemesterResponse>> SearchSemesters(int sem);
        Task<IEnumerable<PaperResponse>> GetQuestionPaperByExamId(Guid id);
        Task<IEnumerable<PreviousPaper>> GetAllPreviousPapers(Guid examId, Guid semesterId);
        Task<IEnumerable<StudentQuestionPaper>> GetQuestionPaperForStudentByExamId(Guid id);
        Task<int> ConductExam(Guid examId);
        Task<int> SubmitStudentResult(Result model);
        Task<IEnumerable<StudentResult>> GetStudentResultByRegNo(ResultRequest model);
        Task<StudentResult?> CheckIfPaperIsALreadySubmittedByStudent(Guid entityId,Guid examId);
      
    }
}
