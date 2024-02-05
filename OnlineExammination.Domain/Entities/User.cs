using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? RegistrationNumber { get; set; } = string.Empty;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string ResetCode { get; set; } = String.Empty;
        public string ContactNo { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public Gender Gender { get; set; }
        public UserRole UserRole { get; set; } = UserRole.Student;
    }
}
