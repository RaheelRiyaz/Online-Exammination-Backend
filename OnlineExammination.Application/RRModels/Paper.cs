using OnlineExammination.Domain.Entities;

namespace OnlineExammination.Application.RRModels
{
    public class PaperRequest
    {
        public Guid ExamId { get; set; }
        public string Question { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public int CorrectOption { get; set; } 
    }
    public class PaperResponse:QuestionPaper
    {
    }

    public class Paper
    {
        public Guid ExamId { get; set; }
        public string Question { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
    }

    public class StudentPaper
    {
        public Guid QuestionId { get; set; }
        public Guid ExamId { get; set; }
        //public string Batch { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string ExamName { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int ExamTotalMarks { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int EachQuestionMarks { get; set; }
    }


    public class SubmitPaper
    {
        public Guid ExamId { get; set; }
        public Guid EntityId { get; set; }
        //public int PassMarks { get; set; }

        public IEnumerable<QuestionAnswer> QuestionAnswers { get; set; } = null!;
    }

    public class QuestionAnswer
    {
        public Guid QuestionId { get; set; }
        public int Answer { get; set; } 
    }

    public class PreviousPaper
    {
        public Guid PaperId { get; set; }
        public Guid ExamId { get; set; }
        public string SubjectName { get; set; } = null!;
        public string Question { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
        public string ProgramName { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public string PaperTitle { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public int Semester { get; set; }
    }


    public class PreviousPaperResponse
    {
        public string SubjectName { get; set; } = null!;
        public Guid ExamId { get; set; }
        public int Semester { get; set; }
        public string ProgramName { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public string PaperTitle { get; set; } = null!;
        public DateTime StartDate { get; set; }

        public IEnumerable<PreviousQuestionPaper> Questions { get; set; } = null!;

    }

    public class StudentPaperResponse:PreviousPaperResponse
    {
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int ExamTotalMarks { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int EachQuestionMarks { get; set; }
    }

    public class PreviousQuestionPaper
    {
        public string Question { get; set; } = null!;
        public Guid Id { get; set; }
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;
    }



    public class StudentQuestionPaper:PreviousPaper
    {
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int ExamTotalMarks { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int EachQuestionMarks { get; set; }
    }
}
