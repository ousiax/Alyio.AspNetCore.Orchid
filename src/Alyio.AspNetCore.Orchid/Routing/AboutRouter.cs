using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Alyio.AspNetCore.Orchid.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Alyio.AspNetCore.Orchid.Routing
{
    public class AboutRouter : ITemplateRouter
    {
        private IHostingEnvironment _hostingEnvironment;
        private ServerOptions _serverOptions;

        public string Template => "about";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RouteAsync(RouteContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            EnsureServices(context.HttpContext);

            context.Handler = async _ =>
            {
                var about = GetAboutText();
                if (!string.IsNullOrEmpty(about))
                {
                    context.HttpContext.Response.ContentType = " text/plain; charset=utf-8";
                    await context.HttpContext.Response.WriteAsync(about);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 404;
                    await context.HttpContext.Response.WriteAsync("Couldn't find the about text.");
                }
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_serverOptions == null)
            {
                _serverOptions = context.RequestServices.GetService<IOptions<ServerOptions>>().Value;
            }
            if (_hostingEnvironment == null)
            {
                _hostingEnvironment = context.RequestServices.GetService<IHostingEnvironment>();
            }
        }

        private string GetAboutText()
        {
            string aboutText = null;

            string[] lookupPaths = new[] {
                _hostingEnvironment.ContentRootPath,
                Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
            };
            if (File.Exists(_serverOptions.About))
            {
                aboutText = File.ReadAllText(_serverOptions.About);
            }
            else
            {
                string fileName = Path.GetFileName(_serverOptions.About);
                if (!string.IsNullOrEmpty(fileName))
                {
                    foreach (var path in lookupPaths)
                    {
                        var file = Path.Combine(path, fileName);
                        if (File.Exists(file))
                        {
                            aboutText = File.ReadAllText(file);
                            break;
                        }
                    }
                }
            }
            return aboutText;
        }
    }
}
