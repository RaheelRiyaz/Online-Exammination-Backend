using Microsoft.Extensions.DependencyInjection;
using OnlineExammination.Application.Abstractions.IServices;
using OnlineExammination.Application.Services;
using System.Reflection;

namespace OnlineExammination.Application
{
    public static class AssemblyReference
    {
        public static  IServiceCollection AddApplicationServices(this IServiceCollection services,string webRootPath)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddSingleton<IStorageService>(new StorageService(webRootPath));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
