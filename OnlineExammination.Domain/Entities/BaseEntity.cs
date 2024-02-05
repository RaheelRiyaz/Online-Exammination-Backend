namespace OnlineExammination.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CeatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
