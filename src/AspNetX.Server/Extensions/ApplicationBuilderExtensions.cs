using System.Reflection;
using AspNetX.Initialization;
using AspNetX.Server.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AspNetX.Server.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAspNetXFileServer(this IApplicationBuilder app)
        {
            app.UseMiddleware<FileMinifierMiddleware>();
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "",
                EnableDefaultFiles = true,
                FileProvider = new EmbeddedFileProvider(typeof(ServerMiddleware).GetTypeInfo().Assembly, "AspNetX.Server.Resources.Embedded.www")
            });
            return app;
        }

        public static IApplicationBuilder UseAspNetXRouter(this IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            var routers = app.ApplicationServices.GetService<IExtensionProvider<ITemplateRouter>>().Instances;
            var inline = app.ApplicationServices.GetService<IInlineConstraintResolver>();
            foreach (var router in routers)
            {
                routeBuilder.Routes.Add(new Route(router, router.RouteTemplate, inline));
            }
            app.UseRouter(routeBuilder.Build());
            return app;
        }
    }
}
