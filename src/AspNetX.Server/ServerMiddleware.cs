using AspNetX.Initialization;
using Microsoft.AspNet.Builder;

namespace AspNetX.Server
{
    internal class ServerMiddleware : IRegisterMiddleware
    {
        public void RegisterMiddleware(IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<AspNetXServerMiddleware>(appBuilder);
        }
    }
}
