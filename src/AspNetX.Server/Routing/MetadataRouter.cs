using System;
using System.Net;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Routing
{
    public class MetadataRouter : ITemplateRouter
    {
        private IModelMetadataWrapperProvider _metadataWrapperProvider;

        public string RouteTemplate => "meta/{id}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            int? id = context.RouteData.Values["id"].ToInt32();
            if (!id.HasValue)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.IsHandled = true;
                return;
            }

            EnsureServices(context.HttpContext);

            IModelMetadataWrapper wrapper;
            if (_metadataWrapperProvider.TryGetModelMetadataWrapper(id.Value, out wrapper))
            {
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                await context.HttpContext.Response.WriteJsonAsync(wrapper);
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status404NotFound;
            }
            context.IsHandled = true;
        }

        private void EnsureServices(HttpContext context)
        {
            if (_metadataWrapperProvider == null)
            {
                _metadataWrapperProvider = context.ApplicationServices.GetService<IModelMetadataWrapperProvider>();
            }
        }
    }
}
