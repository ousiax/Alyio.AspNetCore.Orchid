using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Initialization
{
    public interface IRegisterServices
    {
        void RegisterServices(IServiceCollection services);
    }
}
