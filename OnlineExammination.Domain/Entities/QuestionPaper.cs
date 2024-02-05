using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineExammination.Domain.Entities
{
    public class QuestionPaper : BaseEntity
    {
        public Guid ExamId { get; set; }
        public string Question { get; set; } = null!;
        public string OptionA { get; set; } = null!;
        public string OptionB { get; set; } = null!;
        public string OptionC { get; set; } = null!;
        public string OptionD { get; set; } = null!;

        [JsonIgnore]
        public int CorrectOption { get; set; }
       // public bool IsReleased { get; set; } = false;



        #region Navigation Props
        [ForeignKey(nameof(ExamId))]
        [JsonIgnore]
        public Exam Exams { get; set; } = null!;
        #endregion Navigation Props 
    }
}
