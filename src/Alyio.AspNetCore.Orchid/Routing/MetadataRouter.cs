using System;
using System.Net;
using System.Threading.Tasks;
using Alyio.AspNetCore.Orchid.Abstractions;
using Alyio.AspNetCore.Orchid.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Alyio.AspNetCore.Orchid.Routing
{
    public class MetadataRouter : ITemplateRouter
    {
        private IHostingEnvironment _hostingEnvironment;
        private IModelMetadataWrapperProvider _modelMetadataWrapperProvider;

        public string Template => "meta/{modelTypeId}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RouteAsync(RouteContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            string modelTypeId = context.RouteData.Values["modelTypeId"] as string;
            if (!string.IsNullOrEmpty(modelTypeId))
            {
                EnsureServices(context.HttpContext);
                context.Handler = async _ =>
                {
                    var modelMetadataWrapper = (ModelMetadataWrapper)null;
                    if (_modelMetadataWrapperProvider.TryGet(modelTypeId, out modelMetadataWrapper))
                    {
                        context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                        Formatting formatting = _hostingEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                        await context.HttpContext.Response.WriteJsonAsync(modelMetadataWrapper, formatting);
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"A ModelMetadataWrapper with the specified {modelTypeId} could not be found.");
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
            if (_modelMetadataWrapperProvider == null)
            {
                _modelMetadataWrapperProvider = context.RequestServices.GetService<IModelMetadataWrapperProvider>();
            }
        }
    }
}
