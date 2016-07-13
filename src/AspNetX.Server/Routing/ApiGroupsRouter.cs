using System;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Routing
{
    public class ApiGroupsRouter : ITemplateRouter
    {
        private IApiDescriptionGroupCollectionWrapperProvider _descriptionProvider;

        public string RouteTemplate => "apigroups";

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
                context.HttpContext.Response.ContentType = "application/json";
                var apiGroups = _descriptionProvider.ApiDescriptionGroupsWrapper;
                await context.HttpContext.Response.WriteJsonAsync(apiGroups);
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_descriptionProvider == null)
            {
                _descriptionProvider = context.RequestServices.GetService<IApiDescriptionGroupCollectionWrapperProvider>();
            }
        }
    }
}