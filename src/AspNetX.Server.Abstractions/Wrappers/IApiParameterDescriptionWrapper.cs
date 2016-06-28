using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiParameterDescriptionWrapper
    {
        string Name { get; }

        string Source { get; }

        IModelMetadataWrapper Metadata { get; }

        IApiParameterRouteInfoWrapper RouteInfo { get; }

        ApiParameterDescription ApiParameterDescription { get; }
    }
}
