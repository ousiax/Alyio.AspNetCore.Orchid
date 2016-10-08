using System;
using System.Threading.Tasks;
using DocX.Abstractions;
using DocX.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DocX.Routing
{
    public class ApiRouter : ITemplateRouter
    {
        private IHostingEnvironment _hostingEnvironment;
        private IApiDescriptionDetailModelProvider _apiDescriptionDetailModelProvider;

        public string Template => "api/{id}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RouteAsync(RouteContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            string id = context.RouteData.Values["id"] as string;
            if (!string.IsNullOrEmpty(id))
            {
                EnsureServices(context.HttpContext);

                context.Handler = async _ =>
                {
                    var apiDescriptionDetailModel = (ApiDescriptionDetailModel)null;
                    if (_apiDescriptionDetailModelProvider.TryGetValue(id, out apiDescriptionDetailModel))
                    {
                        context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                        Formatting formatting = _hostingEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                        await context.HttpContext.Response.WriteJsonAsync(apiDescriptionDetailModel, formatting);
                    }
                };
            }
        }

        private void EnsureServices(HttpContext context)
        {
            if (_hostingEnvironment == null)
            {
                _hostingEnvironment = context.RequestServices.GetService<IHostingEnvironment>();
            }
            if (_apiDescriptionDetailModelProvider == null)
            {
                _apiDescriptionDetailModelProvider = context.RequestServices.GetService<IApiDescriptionDetailModelProvider>();
            }
        }
    }
}
