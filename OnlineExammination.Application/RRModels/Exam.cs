namespace OnlineExammination.Application.RRModels
{
    public class ExamRequest
    {
        public Guid ProgramId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SemesterId { get; set; }
        public string Batch { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int ExamTotalMarks { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int EachQuestionMarks { get; set; }
    }

    public class ExamResponse:ExamRequest
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public string ProgramName { get; set; } = null!;
        public int Semester { get; set; } 
        public bool IsConducted { get; set; } 
    }


    public class UpdateExamRequest : ExamRequest
    {
        public Guid Id { get; set; }
    }

    public class Animal
    {
        public virtual string Sound()
        {
            return "Animal Sound";
        }
    }

    public class Cat : Animal
    {
        public override string Sound()
        {
            return "Cat Sound";
        }
    }
}
