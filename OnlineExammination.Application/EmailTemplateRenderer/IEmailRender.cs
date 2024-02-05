using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure.EmailTemplateRenderer
{
    public interface IEmailRender
    {
        Task<string> RenderEmailAsync(string templateName, object model);
    }
}
