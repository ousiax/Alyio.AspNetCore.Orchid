using System;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
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

        public async Task RouteAsync(RouteContext context)
        {
            string id = context.RouteData.Values["id"] as string;
            if (!string.IsNullOrEmpty(id))
            {
                EnsureServices(context.HttpContext);

                var apiDescriptionDetailModel = (ApiDescriptionDetailModel)null;
                if (_apiDescriptionDetailModelProvider.TryGetValue(id, out apiDescriptionDetailModel))
                {
                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    await context.HttpContext.Response.WriteJsonAsync(apiDescriptionDetailModel);
                }
                context.IsHandled = true;
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
