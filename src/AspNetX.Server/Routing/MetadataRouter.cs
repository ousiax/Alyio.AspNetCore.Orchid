using System;
using System.Net;
using System.Threading.Tasks;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
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

        public async Task RouteAsync(RouteContext context)
        {
            string modelTypeId = context.RouteData.Values["modelTypeId"] as string;
            if (!string.IsNullOrEmpty(modelTypeId))
            {
                EnsureServices(context.HttpContext);
                var modelMetadataWrapper = (ModelMetadataWrapper)null;
                if (_modelMetadataWrapperProvider.TryGet(modelTypeId, out modelMetadataWrapper))
                {
                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    await context.HttpContext.Response.WriteJsonAsync(modelMetadataWrapper);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.HttpContext.Response.WriteAsync($"A ModelMetadataWrapper with the specified {modelTypeId} could not be found.");
                }
                context.IsHandled = true;
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
