using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Domain.Entities
{
    public class AppFile:BaseEntity
    {
        public Guid EntityId { get; set; }
        public string FilePath { get; set; } = null!;
        public Module Module { get; set; }
    }
}
