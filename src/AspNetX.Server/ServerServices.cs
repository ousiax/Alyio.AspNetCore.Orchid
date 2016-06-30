using AspNetX.Initialization;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Impl;
using AspNetX.Server.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server
{
    public class ServerServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddOptions();
            services.AddSingleton<IApiDescriptionGroupCollectionWrapperProvider, ApiDescriptionGroupCollectionWrapperProvider>();
            services.AddSingleton<IApiDescriptionWrapperProvider, ApiDescriptionWrapperProvider>();
            services.AddSingleton<IApiModelProvider, ApiModelProvider>();
            services.AddSingleton<IModelMetadataIdentityProvider, ModelMetadataIdentityProvider>();
            services.AddSingleton<IModelMetadataWrapperProvider, ModelMetadataWrapperProvider>();
            services.AddSingleton<IObjectGenerator, ObjectGenerator>();
            services.AddSingleton<IExtensionProvider<ITemplateRouter>, DefaultExtensionProvider<ITemplateRouter>>();
        }
    }
}
