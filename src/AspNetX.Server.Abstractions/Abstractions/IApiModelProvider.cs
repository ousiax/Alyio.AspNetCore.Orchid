using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiModelProvider
    {
        IApiModel GetApiModel(ApiDescription description);
    }
}
