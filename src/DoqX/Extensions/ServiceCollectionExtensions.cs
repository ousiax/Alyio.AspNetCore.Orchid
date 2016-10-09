using DoqX.Abstractions;
using DoqX.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DoqX
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDoqX(this IServiceCollection services)
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
