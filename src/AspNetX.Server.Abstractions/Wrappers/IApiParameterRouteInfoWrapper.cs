using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiParameterRouteInfoWrapper
    {
        object DefaultValue { get; }

        bool? IsOptional { get; }

        ApiParameterRouteInfo ApiParameterRouteInfo { get; }
    }
}
