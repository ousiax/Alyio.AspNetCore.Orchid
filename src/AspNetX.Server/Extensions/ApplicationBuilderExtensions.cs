using System.Reflection;
using AspNetX.Initialization;
using AspNetX.Server.Routing;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAspNetXFileServer(this IApplicationBuilder app)
        {
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
            var routeBuilder = new RouteBuilder { ServiceProvider = app.ApplicationServices };
            var routers = app.ApplicationServices.GetService<IExtensionProvider<ITemplateRouter>>().Instances;
            var inline = app.ApplicationServices.GetService<IInlineConstraintResolver>();
            foreach (var router in routers)
            {
                routeBuilder.Routes.Add(new TemplateRoute(router, router.RouteTemplate, inline));
            }
            app.UseRouter(routeBuilder.Build());
            return app;
        }
    }
}
