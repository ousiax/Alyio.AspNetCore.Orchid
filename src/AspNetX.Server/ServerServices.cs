using AspNetX.Initialization;
using AspNetX.Server.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server
{
    public class ServerServices : IRegisterServices
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton<IApiGroupsProvider, ApiGroupsProvider>();
        }
    }
}
