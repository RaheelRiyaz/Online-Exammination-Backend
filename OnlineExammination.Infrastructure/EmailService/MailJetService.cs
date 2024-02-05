using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;
using OnlineExammination.Application.Abstractions.IEmailService;
using OnlineExammination.Infrastructure.MailOption;
using OnlineExammination.Infrastructure.MailSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure.EmailService
{
    public class MailJetService : IEmailService
    {
        private readonly MailJetoption options;

        public MailJetService(IOptions<MailJetoption> options)
        {
            this.options = options.Value;
        }
        public async Task<bool> SendEmailAsync(EmailSetting emailSetting)
        {
            MailjetClient mailjetClient = new MailjetClient(options.ApiKey,options.SescretKey);
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(options.FromEmail)).
                WithTo(new SendContact(emailSetting.To.FirstOrDefault())).
                WithHtmlPart(emailSetting.Body).
                Build();

            await mailjetClient.SendTransactionalEmailAsync(email);

            return true;
        }
    }
}
