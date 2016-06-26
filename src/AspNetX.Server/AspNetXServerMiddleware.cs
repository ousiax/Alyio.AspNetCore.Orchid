using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetX.Server
{
    internal class AspNetXServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationBuilder _appBuilder;

        public AspNetXServerMiddleware(RequestDelegate next, IApplicationBuilder appBuilder)
        {
            this._appBuilder = appBuilder;
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiGroups = context.ApplicationServices.GetService<IApiGroupsProvider>();
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(apiGroups.ApiXDescriptionGroups, Formatting.Indented));
        }
    }
}
