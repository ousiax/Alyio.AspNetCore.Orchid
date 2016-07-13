using System;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Routing
{
    public class ApiRouter : ITemplateRouter
    {
        private IApiDescriptionWrapperProvider _descriptionProvider;
        private IApiModelProvider _apiModelProvider;

        public string RouteTemplate => "api/{id}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RouteAsync(RouteContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            string id = context.RouteData.Values["id"] as string;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            EnsureServices(context.HttpContext);

            context.Handler = async ctx =>
            {
                var api = (IApiDescriptionWrapper)null;
                _descriptionProvider.TryGetValue(id, out api);
                ctx.Response.ContentType = "application/json";
                await ctx.Response.WriteJsonAsync(_apiModelProvider.GetApiModel(api.ApiDescription));
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_descriptionProvider == null)
            {
                _descriptionProvider = context.RequestServices.GetService<IApiDescriptionWrapperProvider>();
            }
            if (_apiModelProvider == null)
            {
                _apiModelProvider = context.RequestServices.GetService<IApiModelProvider>();
            }
        }
    }
}
