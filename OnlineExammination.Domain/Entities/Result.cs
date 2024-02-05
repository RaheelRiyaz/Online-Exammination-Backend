using OnlineExammination.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExammination.Domain.Entities
{
    public class Result:BaseEntity
    {
        public Guid EntityId { get; set; }
        public Guid ExamId { get; set; }
        public int Marks { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public int IsReleased { get; set; }
        public int TotalAttempts { get; set; }



        #region Navigation Properties

        [ForeignKey(nameof(EntityId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; } = null!;
        #endregion  Navigation Properties
    }
}
