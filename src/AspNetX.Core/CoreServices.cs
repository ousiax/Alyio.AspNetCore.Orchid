using System.Collections.Generic;
using AspNetX.Initialization;
using AspNetX.Internal;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX
{
    public class CoreServices
    {
        public static IEnumerable<ServiceDescriptor> GetDefaultServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITypeActivator, DefaultTypeActivator>();
            services.AddSingleton<ITypeSelector, DefaultTypeSelector>();
            services.AddSingleton<ITypeService, DefaultTypeService>();
            services.AddSingleton<IAssemblyProvider, DefaultAssemblyProvider>();

            services.AddSingleton<IExtensionProvider<IRegisterServices>, DefaultExtensionProvider<IRegisterServices>>();
            services.AddSingleton<IExtensionProvider<IRegisterMiddleware>, DefaultExtensionProvider<IRegisterMiddleware>>();
            services.AddSingleton<IExtensionProvider<IApplicationModelConvention>, DefaultExtensionProvider<IApplicationModelConvention>>();
            return services;
        }
    }
}
