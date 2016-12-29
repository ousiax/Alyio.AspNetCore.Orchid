using System.Reflection;
using Orchid.Abstractions;
using Orchid.Conventions;
using Orchid.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Orchid
{
    /// <summary>
    /// Extension methods for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Apply the Orchid to generate the help pages.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOrchid(this IApplicationBuilder app)
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
                FileProvider = new EmbeddedFileProvider(
                    typeof(ApplicationBuilderExtensions).GetTypeInfo().Assembly, "Alyio.AspNetCore.Orchid.www")
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
                new MetadataRouter(),
                new AboutRouter()
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
