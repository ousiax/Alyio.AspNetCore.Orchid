using AspNetX.Initialization;
using AspNetX.Server.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.OptionsModel;

namespace AspNetX.Server
{
    internal class ServerMiddleware : IRegisterMiddleware
    {
        private readonly AspNetXServerOptions _serverOptions;

        public ServerMiddleware(IOptions<AspNetXServerOptions> serverOptions)
        {
            this._serverOptions = serverOptions.Value;
        }

        public void RegisterMiddleware(IApplicationBuilder appBuilder)
        {
            appBuilder.Map($"/{_serverOptions.BasePath}", app =>
            {
                app.UseAspNetXFileServer();
                app.UseAspNetXRouter();
            });
        }
    }
}
