using System;
using Alyio.AspNetCore.Orchid.Abstractions;
using Alyio.AspNetCore.Orchid.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alyio.AspNetCore.Orchid
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

        public static IServiceCollection AddOrchid(this IServiceCollection services, Action<OrchidOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            return services.AddOrchid().Configure(setupAction);
        }
    }
}
