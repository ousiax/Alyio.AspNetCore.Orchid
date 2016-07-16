using System;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Routing
{
    public class ApiGroupsRouter : ITemplateRouter
    {
        private IApiDescriptionGroupModelCollectionProvider _apiDescriptionGroupModelCollectionProvider;

        public string Template => "apigroups";

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
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                var apiGroups = _apiDescriptionGroupModelCollectionProvider.ApiDescriptionGroups;
                await context.HttpContext.Response.WriteJsonAsync(apiGroups);
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_apiDescriptionGroupModelCollectionProvider == null)
            {
                _apiDescriptionGroupModelCollectionProvider = context.RequestServices.GetService<IApiDescriptionGroupModelCollectionProvider>();
            }
        }
    }
}
