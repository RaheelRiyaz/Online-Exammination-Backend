using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineExammination.Application.Abstractions.IEmailService;
using OnlineExammination.Infrastructure.EmailRenderrService;
using OnlineExammination.Infrastructure.EmailService;
using OnlineExammination.Infrastructure.EmailTemplateRenderer;
using OnlineExammination.Infrastructure.MailOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailJetoption>(configuration.GetSection("MailJetOptionSection"));
            services.AddScoped<IEmailService, MailJetService>();
            services.AddScoped<IEmailRender, EmailRenderer>();
            return services;
        }
    }
}
