using System;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
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

        public async Task RouteAsync(RouteContext context)
        {
            EnsureServices(context.HttpContext);
            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            var apiGroups = _apiDescriptionGroupModelCollectionProvider.ApiDescriptionGroups;
            await context.HttpContext.Response.WriteJsonAsync(apiGroups);
            context.IsHandled = true;
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
