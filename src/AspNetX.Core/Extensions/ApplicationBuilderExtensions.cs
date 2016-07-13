using AspNetX.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetX
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAspNetX(this IApplicationBuilder appBuilder)
        {
            var middlewareRegistrations = appBuilder.ApplicationServices.GetService<IExtensionProvider<IRegisterMiddleware>>();

            foreach (var instance in middlewareRegistrations.Instances)
            {
                instance.RegisterMiddleware(appBuilder);
            }

            MvcOptions mvcOptions = appBuilder.ApplicationServices.GetService<IOptions<MvcOptions>>().Value;
            var extensionProvider = appBuilder.ApplicationServices.GetService<IExtensionProvider<IApplicationModelConvention>>();

            foreach (var convention in extensionProvider.Instances)
            {
                mvcOptions.Conventions.Add(convention);
            }

            return appBuilder;
        }
    }
}
