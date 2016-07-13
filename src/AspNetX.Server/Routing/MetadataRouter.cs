using System;
using System.Net;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task RouteAsync(RouteContext context)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int? id = context.RouteData.Values["id"].ToInt32();
            if (!id.HasValue)
            {

                context.Handler = async _ =>
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.HttpContext.Response.WriteAsync(HttpStatusCode.BadRequest.ToString());
                };
                return;
            }

            EnsureServices(context.HttpContext);

            context.Handler = async _ =>
            {
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
            };
        }

        private void EnsureServices(HttpContext context)
        {
            if (_metadataWrapperProvider == null)
            {
                _metadataWrapperProvider = context.RequestServices.GetService<IModelMetadataWrapperProvider>();
            }
        }
    }
}
