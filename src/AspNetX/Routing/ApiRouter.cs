using System;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Routing
{
    public class ApiRouter : ITemplateRouter
    {
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
                        await context.HttpContext.Response.WriteJsonAsync(apiDescriptionDetailModel);
                    }
                };
            }
        }

        private void EnsureServices(HttpContext context)
        {
            if (_apiDescriptionDetailModelProvider == null)
            {
                _apiDescriptionDetailModelProvider = context.RequestServices.GetService<IApiDescriptionDetailModelProvider>();
            }
        }
    }
}
