using OnlineExammination.Infrastructure.EmailTemplateRenderer;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExammination.Infrastructure.EmailRenderrService
{
    public class EmailRenderer : IEmailRender
    {
        public async Task<string> RenderEmailAsync(string templateName, object model)
        {
            string template = string.Empty;

            try
            {
                var assembly = Assembly.GetExecutingAssembly().Location;
                var folder = Path.GetDirectoryName(assembly);
                var templateFolder = string.Concat(folder, "EmailTemplates");

                var razorLightEngine = new RazorLightEngineBuilder().
                    UseFileSystemProject(templateFolder).
                    EnableDebugMode().
                    UseMemoryCachingProvider().
                    Build()
                    ;

                await razorLightEngine.CompileRenderAsync(templateName, model);
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return template;
        }
    }
}
