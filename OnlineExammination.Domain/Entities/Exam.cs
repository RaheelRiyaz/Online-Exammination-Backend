using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExammination.Domain.Entities
{
    public class Exam:BaseEntity
    {
        public Guid ProgramId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SemesterId { get; set; }
        public string Name { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int ExamDuration { get; set; }
        public int ExamPassMarks { get; set; }
        public int ExamTotalMarks { get; set; }
        public int TotalNoOfQuestions { get; set; }
        public int EachQuestionMarks { get; set; }
        public bool IsConducted { get; set; }

        #region Navigation Props

        [ForeignKey(nameof(ProgramId))]
        public Program Program { get; set; } = null!;

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;

        [ForeignKey(nameof(SemesterId))]
        public Semester Semester { get; set; } = null!;
        #endregion Navigation Props
    }
}
