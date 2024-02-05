using OnlineExammination.Domain.Enums;

namespace OnlineExammination.Application.RRModels
{
    public class StudentSignupRequest : AdminSignupRequest
    {
        public string RegistrationNumber { get; set; } = null!;

    }

    public class StudentSignupResponse : AdminSignupResponse
    {
        public string RegistrationNumber { get; set; } = null!;

    }

    public class AdminSignupRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public Gender Gender { get; set; }
    }
    public class AdminSignupResponse
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public Gender Gender { get; set; }

        public string ContactNo { get; set; } = null!;
    }


    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public UserRole UserRole { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
    public class LoginRequest
    {
        public string UserNameOrRegNo { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = null!;
    }
}
