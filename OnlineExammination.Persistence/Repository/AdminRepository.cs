using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;

namespace OnlineExammination.Persistence.Repository
{
    public class AdminRepository : BaseRepository<User>, IAdmin
    {
        private readonly string baseQuery = $@"SELECT P.Id as PaperId ,ExamId, S.Name as SubjectName ,
                            Question ,OptionA ,OptionB ,OptionC, OptionD ,Pr.Name as ProgramName ,
                            Batch , StartDate , E.Description as PaperTitle ,Sem.Sem as Semester
                            FROM Papers P INNER JOIN Exams E
                            INNER JOIN Subjects S ON S.Id=E.SubjectId
                            INNER JOIN Programs Pr ON Pr.ID = E.ProgramId
                            INNER JOIN Semesters Sem ON Sem.Id = E.SemesterId
                            ON E.Id = P.ExamId ";
        public AdminRepository(OnlineExamminationDbContext context) : base(context) { }

        public async Task<int> AddPaper(QuestionPaper model)
        {
            string query = $@"INSERT INTO Papers VALUES
                            (
                            @id,
                            @examId,
                            @question,
                            @optionA,
                            @optionB,
                            @optionC,
                            @optionD,
                            @correctOption,
                            @ceatedOn,
                            @updatedOn 
                            )";
            return await context.ExecuteAsync(query, new { 
                model.Id, 
                model.ExamId, 
                model.Question,
                model.OptionA, 
                model.OptionB, 
                model.OptionC,
                model.OptionD, 
                model.CorrectOption, 
                model.CeatedOn,
                model.UpdatedOn
            });
        }

        public async Task<int> AddProgram(Program model)
        {
            string query = $@"INSERT INTO Programs 
                            VALUES 
                            (
                            @id,
                            @name,
                            @createdOn,
                            @updatedOn
                            )";
            return await context.ExecuteAsync(query, new { id = model.Id, name = model.Name, createdOn = model.CeatedOn, model.UpdatedOn });
        }

        public async Task<int> AddSemester(Semester model)
        {
            string query = $@"INSERT INTO Semesters 
                            VALUES 
                            (
                            @id,
                            @sem,
                            @createdOn,
                            @updatedOn
                            )";
            return await context.ExecuteAsync(query, new { model.Id, model.Sem, createdOn = model.CeatedOn, model.UpdatedOn });
        }

        public async Task<IEnumerable<Program>> GetPrograms()
        {
            return await context.QueryAsync<Program>("Select * from Programs");
        }

        public async Task<IEnumerable<PaperResponse>> GetQuestionPaperByExamId(Guid id)
        {
            string query = $@"SELECT * FROM Papers WHERE ExamId=@id ";
            return await context.QueryAsync<PaperResponse>(query, new { id });
        }

        public async Task<IEnumerable<StudentQuestionPaper>> GetQuestionPaperForStudentByExamId(Guid id)
        {
            string query =   $@"SELECT P.Id as PaperId ,ExamId, S.Name as SubjectName ,
                                Question ,OptionA ,OptionB ,OptionC, OptionD ,Pr.Name as ProgramName ,
                                Batch , StartDate , E.Description as PaperTitle ,Sem.Sem as Semester ,
                                E.ExamDuration , E.ExamPassMarks ,E.ExamTotalMarks ,E.EachQuestionMarks ,
                                E.TotalNoOfQuestions 
                                FROM Papers P INNER JOIN Exams E
                                INNER JOIN Subjects S ON S.Id = E.SubjectId
                                INNER JOIN Programs Pr ON Pr.ID = E.ProgramId
                                INNER JOIN Semesters Sem ON Sem.Id = E.SemesterId
                                ON E.Id = P.ExamId
                                WHERE E.Id= @id
                                AND IsConducted = 1
                                AND BATCH ='{DateTime.Today.Year}'";


            return await context.QueryAsync<StudentQuestionPaper>(query, new { id });
        }

        public async Task<IEnumerable<Semester>> GetSemesters()
        {
            return await context.QueryAsync<Semester>("Select * from Semesters");
        }

        public async Task<int> ConductExam(Guid examId)
        {
            string query = $@"UPDATE Exams SET
                          IsConducted=1 
                          WHERE Id=@examId" ;
            return await context.ExecuteAsync(query, new { examId });
        }

        public async Task<int> SubmitStudentResult(Result model)
        {
            string query = $@"INSERT INTO Results
				VALUES
				(
				@id,
				@entityId,
				@examId,
				@marks,
				@resultStatus,
				@isReleased,
				@ceatedOn,
				@updatedOn,
                @totalAttempts
				)";

            return await context.ExecuteAsync(query,new { 
                model.Id ,
                model.EntityId ,
                model.ExamId,
                model.Marks,
                model.ResultStatus,
                model.IsReleased,
                model.CeatedOn,
                model.UpdatedOn,
                model.TotalAttempts
            });
        }

        public async Task<IEnumerable<StudentResult>> GetStudentResultByRegNo(ResultRequest model)
        {
            string query = $@"
			    SELECT R.Id as ResultId,
				E.ProgramId ,S.Name as SubjectName,
				R.CeatedOn as ResultUploadDate, 
				E.TotalNoOfQuestions, E.ExamTotalMarks,
				E.ExamDuration ,E.ExamPassMarks ,
				R.Marks as MarksObtained ,
				R.ResultStatus ,E.Id as ExamId,
                R.TotalAttempts
				FROM Exams E
				INNER JOIN Results R ON
				E.Id = R.ExamId
				INNER JOIN Subjects S ON
				S.Id=SubjectId INNER JOIN
				Users U ON U.Id = EntityId
				WHERE 
				U.RegistrationNumber = @registrationNumber
                AND SemesterId = @semesterid
                AND Programid = @programId
				AND IsReleased = 1  ";

            return await context.QueryAsync<StudentResult>(query, new { model.RegistrationNumber , model.Semesterid , model.ProgramId });
        }

        public async Task<IEnumerable<PreviousPaper>> GetAllPreviousPapers(Guid examId,Guid semesterId)
        {
            string query = $@"{baseQuery} 
                            WHERE E.Id = @examId
                            AND SemesterId = @semesterId 
                            AND IsReleased=1 ";

            return await context.QueryAsync<PreviousPaper>(query,new { examId , semesterId});
        }

        public async Task<StudentResult?> CheckIfPaperIsALreadySubmittedByStudent(Guid entityId,Guid examId)
        {
            string query = $@"SELECT * FROM Results WHERE EntityId = @entityId  AND ExamId = @examId";

            return await context.FirstOrDefaulyAsync<StudentResult>(query, new { entityId , examId });
        }

        public async Task<IEnumerable<ProgramResponse>> SearchPrograms(string term)
        {
            string query = $@"select * from programs where name like '{term}%'";

            return await context.QueryAsync<ProgramResponse>(query);
        }

        public async Task<IEnumerable<SemesterResponse>> SearchSemesters(int sem)
        {
            string query = $@"select * from semesters where sem = @sem";

            return await context.QueryAsync<SemesterResponse>(query,new {sem});
        }
    }
}
