using OnlineExammination.Infrastructure.MailSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Application.Abstractions.IEmailService
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailSetting emailSetting);
    }
}
