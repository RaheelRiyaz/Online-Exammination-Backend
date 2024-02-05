using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure.MailOption
{
    public class MailJetoption
    {
        public string ApiKey { get; set; } = null!;
        public string SescretKey { get; set; } = null!;
        public string FromName { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
    }
}
