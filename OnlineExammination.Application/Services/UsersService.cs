using AutoMapper;
using OnlineExammination.Application.Abstractions.IEmailService;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.RRModels;
using OnlineExammination.Application.Shared;
using OnlineExammination.Application.Utilis;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Domain.Enums;
using OnlineExammination.Infrastructure.EmailTemplateRenderer;
using OnlineExammination.Infrastructure.MailSetting;
using System.Net;

namespace OnlineExammination.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IFileService service;
        private readonly IEmailService emailService;
        private readonly IEmailRender emailRender;

        public UsersService(IUserRepository repository, IMapper mapper, IFileService service, IEmailService emailService ,IEmailRender emailRender)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.service = service;
            this.emailService = emailService;
            this.emailRender = emailRender;
        }
        public async Task<ApiResponse<AdminSignupResponse>> AdminSignup(AdminSignupRequest model)
        {
            var admin = new User()
            {
                ContactNo = model.ContactNo,
                Email = model.Email,
                Gender = model.Gender,
                Name = model.Name,
                Password = model.Password,
                RegistrationNumber = null,
                UserName = model.UserName,
                UserRole = UserRole.Admin
            };
            var res = await repository.Add(admin);
            if (res > 0)
            {

                return new ApiResponse<AdminSignupResponse>()
                {
                    IsSuccess = true,
                    Message = "Signed up successfully",
                    Result = new AdminSignupResponse()
                    {
                        ContactNo = model.ContactNo,
                        Email = model.Email,
                        Gender = model.Gender,
                        Name = model.Name,
                        UserName = model.UserName,
                    },
                    StatusCode = HttpStatusCode.Created
                };
            }
            return new ApiResponse<AdminSignupResponse>()
            {
                IsSuccess = false,
                Message = "There is some issue ",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }




        public async Task<ApiResponse<string>> FogotPassword(string email)
        {
            var user = await repository.FirstOrDefaultAsync(_=>_.Email == email);
            if (user is null) return new ApiResponse<string>()
            {
                Message = "Invalid credentials"
            };

            var emailSetting = new EmailSetting()
            {
                To = new List<string>() { user.Email },
                Body = await emailRender.RenderEmailAsync("ForgotPassword.cshtml", user),
                Subject = "Forgot password"
            };

            var res = await emailService.SendEmailAsync(emailSetting);
            if (res) return new ApiResponse<string> { IsSuccess = true, Message = "Reset code has been sent to your email" };

            return new ApiResponse<string>()
            {
                Message = "Internal server error"
            };
        }




        public async Task<bool> IsUsernameTaken(string username)
        {
            return await repository.IsExists(_ => _.UserName == username);
        }

        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest model)
        {
            var user = (await repository.Filter(_ => _.UserName == model.UserNameOrRegNo || _.RegistrationNumber == model.UserNameOrRegNo)).FirstOrDefault();

            if (user is not null && AppEncryption.ComparePassword(user.Password, user.Salt, model.Password))
            {
                var entityFile = await service.GetFilePathByEntityId(user.Id);
                var response = mapper.Map<LoginResponse>(user);
                if (entityFile != null) { response.FilePath = entityFile.FilePath; }
                else response.FilePath = "/Files/user.jpg";
                return new ApiResponse<LoginResponse>()
                {
                    IsSuccess = true,
                    Message = "Logged In Successfully",
                    Result = response,
                    StatusCode = HttpStatusCode.OK
                };
            }


            return new ApiResponse<LoginResponse>()
            {
                Message = "Invalid credentials"
            };
        }

        public async Task<ApiResponse<StudentSignupResponse>> StudentSignup(StudentSignupRequest model)
        {
            if (await repository.IsExists(_ => _.UserName == model.UserName)) return new ApiResponse<StudentSignupResponse>() { Message = "Username already taken", StatusCode = HttpStatusCode.BadRequest };
            var student = new User()
            {
                ContactNo = model.ContactNo,
                Email = model.Email,
                Gender = model.Gender,
                Name = model.Name,
                Salt = AppEncryption.GenerateSalt(),
                RegistrationNumber = model.RegistrationNumber,
                UserName = model.UserName,
            };
            student.Password = AppEncryption.GenerateHashPassword(student.Salt, model.Password);

            var res = await repository.Add(student);
            if (res > 0)
            {

                return new ApiResponse<StudentSignupResponse>()
                {
                    IsSuccess = true,
                    Message = "Signed up successfully",
                    Result = new StudentSignupResponse()
                    {
                        ContactNo = model.ContactNo,
                        Email = model.Email,
                        Gender = model.Gender,
                        Name = model.Name,
                        UserName = model.UserName,
                        RegistrationNumber = model.RegistrationNumber,
                    },
                    StatusCode = HttpStatusCode.Created
                };
            }
            return new ApiResponse<StudentSignupResponse>()
            {
                IsSuccess = false,
                Message = "There is some issue ",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}
