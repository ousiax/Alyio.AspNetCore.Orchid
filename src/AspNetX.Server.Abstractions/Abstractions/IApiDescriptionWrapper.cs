using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiDescriptionWrapper
    {
        ApiDescription ApiDescription { get; }

        string Id { get; }

        string GroupName { get; }

        string HttpMethod { get; }

        string RelativePath { get; }

        IModelMetadataWrapper ResponseModelMetadataWrapper { get; }
    }
}
