using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace AspNetX.Server
{
    internal class AspNetXServerMiddleware
    {
        private readonly RequestDelegate _next;

        public AspNetXServerMiddleware(RequestDelegate next, IApplicationBuilder appBuilder)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}
