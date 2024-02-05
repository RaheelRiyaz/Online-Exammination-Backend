using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class ExamRepository:BaseRepository<Exam>,IExamRepository
    {
        public ExamRepository(OnlineExamminationDbContext context):base(context) { }

		private readonly string baseQuery = $@"
											  select E.Id ,P.Id as ProgramId,
											  S.Id as SubjectId,
											  E.[Name] as [Name],
											  E.[Description],
											  E.StartDate,E.EndDate,
											  E.ExamDuration,
											  E.ExamPassMarks,
											  E.Batch,
											  E.ExamTotalMarks,
											  E.TotalNoOfQuestions,
											  E.EachQuestionMarks,
										      E.IsConducted,
											  SB.[Name] as SubjectName,
											  P.[Name] as ProgramName,
											  S.Sem as Semester
											  from Exams E inner join Programs P
											  on P.Id=E.ProgramId inner join Semesters S
											  on S.Id=E.SemesterId inner join Subjects SB
											  on Sb.Id=E.SubjectId  ";


        public async Task<ExamResponse?> GetCompactExamById(Guid id)
        {
			return await context.FirstOrDefaulyAsync<ExamResponse>("SELECT * FROM EXAMS WHERE Id=@id", new { id });
        }

        public async Task<IEnumerable<ExamResponse>> GetCompactExams()
        {
			return await context.QueryAsync<ExamResponse>(baseQuery);
        }
    }
}
