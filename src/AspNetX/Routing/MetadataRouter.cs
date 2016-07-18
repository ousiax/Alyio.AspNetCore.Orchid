using System;
using System.Net;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Routing
{
    public class MetadataRouter : ITemplateRouter
    {
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
                        await context.HttpContext.Response.WriteJsonAsync(modelMetadataWrapper);
                    }
                    else
                    {
                        context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                        await context.HttpContext.Response.WriteJsonAsync($"A ModelMetadataWrapper with the specified {modelTypeId} could not be found.");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                };
            }
        }

        private void EnsureServices(HttpContext context)
        {
            if (_modelMetadataWrapperProvider == null)
            {
                _modelMetadataWrapperProvider = context.RequestServices.GetService<IModelMetadataWrapperProvider>();
            }
        }
    }
}
