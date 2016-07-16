using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetX
{
    public class FileMinifierMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDictionary<string, string> _minifierMap;

        public FileMinifierMiddleware(RequestDelegate next, IHostingEnvironment hostingEnvironment)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;

            _minifierMap = new Dictionary<string, string>
            {
                {"bootstrap.css", "bootstrap.min.css" },
                {"main.css", "main.min.css" },
                {"aspnetx.js", "aspnetx.min.js" },
                {"jquery.js", "jquery.min.js" },
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_hostingEnvironment.IsDevelopment()) // Minify static files.
            {
                string fileName = Path.GetFileName(context.Request.Path);
                var minFileName = (string)null;
                if (_minifierMap.TryGetValue(fileName, out minFileName))
                {
                    var minRequestPath = Path.Combine(Path.GetDirectoryName(context.Request.Path), minFileName).Replace("\\", "/");
                    context.Request.Path = minRequestPath;
                }
            }

            await _next.Invoke(context);
        }
    }
}
