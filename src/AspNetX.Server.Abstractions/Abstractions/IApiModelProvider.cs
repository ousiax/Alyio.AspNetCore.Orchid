using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiModelProvider
    {
        IApiModel GetApiModel(ApiDescription description);
    }
}
