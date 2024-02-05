using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure.MailSetting
{
    public class EmailSetting
    {
        public List<string> To { get; set; } = null!;

        public List<string> CC { get; set; } = null!;

        public List<string> BCC { get; set; } = null!;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public List<Attachment> Attachments { get; set; } = null!;
    }
}
