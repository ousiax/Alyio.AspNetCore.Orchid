using AspNetX.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetX
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiExplorer(this IServiceCollection services)
        {
            services.TryAdd(CoreServices.GetDefaultServices());
            var extensionProvider = services.BuildServiceProvider().GetService<IExtensionProvider<IRegisterServices>>();

            foreach (var registration in extensionProvider.Instances)
            {
                registration.RegisterServices(services);
            }
            return services;
        }
    }
}
