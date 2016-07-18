using System.Reflection;
using AspNetX.Abstractions;
using AspNetX.Conventions;
using AspNetX.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace AspNetX
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAspNetX(this IApplicationBuilder app)
        {
            MvcOptions mvcOptions = app.ApplicationServices.GetService<IOptions<MvcOptions>>().Value;
            mvcOptions.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());

            var options = app.ApplicationServices.GetService<IOptions<ServerOptions>>().Value;

            app.Map($"/{options.BasePath}", xApp =>
            {
                xApp.UseXFileServer();
                xApp.UseXRouter();
            });

            return app;
        }

        private static IApplicationBuilder UseXFileServer(this IApplicationBuilder app)
        {
            app.UseMiddleware<FileMinifierMiddleware>();
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "",
                EnableDefaultFiles = true,
                FileProvider = new EmbeddedFileProvider(typeof(ApplicationBuilderExtensions).GetTypeInfo().Assembly, "AspNetX.Resources.Embedded.www")
            });
            return app;
        }

        private static IApplicationBuilder UseXRouter(this IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            var inlineConstraintResolver = app.ApplicationServices.GetService<IInlineConstraintResolver>();
            var routers = new ITemplateRouter[] {
                new ApiGroupsRouter(),
                new ApiRouter(),
                new MetadataRouter()
            };
            foreach (var router in routers)
            {
                routeBuilder.Routes.Add(new Route(router, router.Template, inlineConstraintResolver));
            }

            app.UseRouter(routeBuilder.Build());
            return app;
        }
    }
}
