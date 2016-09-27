using AspNetX.Abstractions;
using AspNetX.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAspNetX(this IServiceCollection services)
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
