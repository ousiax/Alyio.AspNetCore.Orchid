using System;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
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

        public async Task RouteAsync(RouteContext context)
        {
            EnsureServices(context.HttpContext);

            context.HttpContext.Response.ContentType = "application/json";
            var apiGroups = _descriptionProvider.ApiXDescriptionGroups;
            await context.HttpContext.Response.WriteJsonAsync(apiGroups);
            context.IsHandled = true;
        }

        private void EnsureServices(HttpContext context)
        {
            if (_descriptionProvider == null)
            {
                _descriptionProvider = context.ApplicationServices.GetService<IApiDescriptionGroupCollectionWrapperProvider>();
            }
        }
    }
}