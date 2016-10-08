using DocX.Abstractions;
using DocX.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DocX
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocX(this IServiceCollection services)
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
