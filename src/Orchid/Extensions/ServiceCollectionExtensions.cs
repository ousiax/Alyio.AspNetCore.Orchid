using Orchid.Abstractions;
using Orchid.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Orchid
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrchid(this IServiceCollection services)
        {
            services.AddSingleton<IApiDescriptionGroupModelCollectionProvider, ApiDescriptionGroupModelCollectionProvider>();
            services.AddSingleton<IApiDescriptionDetailModelProvider, ApiDescriptionDetailModelProvider>();
            services.AddSingleton<IModelMetadataWrapperProvider, ModelMetadataWrapperProvider>();
            services.AddSingleton<IObjectGenerator, ObjectGenerator>();
            services.AddSingleton<IDocumentationProvider, XmlDocumentationProvider>();
            return services;
        }
    }
}
