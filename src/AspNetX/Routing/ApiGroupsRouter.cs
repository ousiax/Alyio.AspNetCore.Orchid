using System;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetX.Routing
{
    public class ApiGroupsRouter : ITemplateRouter
    {
        private IHostingEnvironment _hostingEnvironment;
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
                Formatting formatting = _hostingEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                await context.HttpContext.Response.WriteJsonAsync(apiGroups, formatting);
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_hostingEnvironment == null)
            {
                _hostingEnvironment = context.RequestServices.GetService<IHostingEnvironment>();
            }
            if (_apiDescriptionGroupModelCollectionProvider == null)
            {
                _apiDescriptionGroupModelCollectionProvider = context.RequestServices.GetService<IApiDescriptionGroupModelCollectionProvider>();
            }
        }
    }
}
