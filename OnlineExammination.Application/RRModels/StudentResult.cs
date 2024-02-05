using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Application.RRModels
{
    public class StudentResult
    {
        public Guid ResultId { get; set; }
        public Guid ExamId { get; set; }
        public Guid ProgramId { get; set; }
        public string SubjectName { get; set; } = null!;
        public DateTime ResultUploadDate { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int ExamTotalMarks { get; set; }
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int MarksObtained { get; set; }
        public int TotalAttempts { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }

    public class ResultRequest
    {
        public string RegistrationNumber { get; set; } = null!;
        public Guid Semesterid { get; set; }
        public Guid ProgramId { get; set; }
    }


    public class UploadResultResquest
    {
        public Guid ExamId { get; set; }
    }
}
