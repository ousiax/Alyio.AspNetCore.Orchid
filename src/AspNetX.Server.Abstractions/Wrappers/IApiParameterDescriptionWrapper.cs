using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiParameterDescriptionWrapper
    {
        string Name { get; }

        string Source { get; }

        IModelMetadataWrapper MetadataWrapper { get; }

        IApiParameterRouteInfoWrapper RouteInfo { get; }

        ApiParameterDescription ApiParameterDescription { get; }
    }
}
