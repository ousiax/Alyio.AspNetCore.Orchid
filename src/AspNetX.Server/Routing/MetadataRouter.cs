using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace AspNetX.Server.Routing
{
    public class MetadataRouter : ITemplateRouter
    {
        public string RouteTemplate => "metadata/{id}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            int? id = context.RouteData.Values["id"].ToInt32();
            if (id == null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.IsHandled = true;
                return;
            }

            EnsureServices(context.HttpContext);

            context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            await context.HttpContext.Response.WriteJsonAsync(context);
            context.IsHandled = true;
        }

        private void EnsureServices(HttpContext context)
        {
        }
    }
}
