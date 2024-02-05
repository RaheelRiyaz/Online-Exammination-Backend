using Microsoft.AspNetCore.Mvc;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;

namespace OnlineExammination.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse<StudentSignupResponse>> StudentSignup(StudentSignupRequest model)
        {
            return await this.service.StudentSignup(model);
        }


        [HttpPost("login")]
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
        {
            return await this.service.Login(model);
        }

        [HttpPost("admin/signup")]
        public async Task<ApiResponse<AdminSignupResponse>> AdminSignup(AdminSignupRequest model)
        {
            return await this.service.AdminSignup(model);
        }

        [HttpGet("check/{username}")]
        public async Task<bool> CheckUsername(string username)
        {
            return await service.IsUsernameTaken(username);
        }

        [HttpPost("forgot-password")]
        public async Task<ApiResponse<string>> ForgotPassword(ForgotPasswordRequest model)
        {
            return await service.FogotPassword(model.Email);
        }
    }
}
