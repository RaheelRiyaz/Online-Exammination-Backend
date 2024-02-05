using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Persistence.Data;
using OnlineExammination.Persistence.Repository;

namespace OnlineExammination.Persistence
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuartion)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdmin, AdminRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddDbContextPool<OnlineExamminationDbContext>((options) =>
            {
                options.UseSqlServer(configuartion.GetConnectionString("OnlineExamminationDbContext"));
            });
            return services;
        }
    }
}
