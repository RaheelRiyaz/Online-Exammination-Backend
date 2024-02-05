using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Application.Abstractions.IServices
{
    public interface IUsersService
    {
        Task<ApiResponse<StudentSignupResponse>> StudentSignup(StudentSignupRequest model);
        Task<ApiResponse<AdminSignupResponse>> AdminSignup(AdminSignupRequest model);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest model);
        Task<bool> IsUsernameTaken(string username);
        Task<ApiResponse<string>> FogotPassword(string email);
    }
}
