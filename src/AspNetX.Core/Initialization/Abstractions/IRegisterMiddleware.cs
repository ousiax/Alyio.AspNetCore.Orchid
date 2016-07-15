using Microsoft.AspNetCore.Builder;

namespace AspNetX.Initialization
{
    public interface IRegisterMiddleware
    {
        void RegisterMiddleware(IApplicationBuilder appBuilder);
    }
}
