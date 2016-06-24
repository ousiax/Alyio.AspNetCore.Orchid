using Microsoft.AspNet.Builder;

namespace AspNetX.Initialization
{
    public interface IRegisterMiddleware
    {
        void RegisterMiddleware(IApplicationBuilder appBuilder);
    }
}
