using AspNetX.Initialization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace AspNetX
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiExplorer(this IApplicationBuilder appBuilder)
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
