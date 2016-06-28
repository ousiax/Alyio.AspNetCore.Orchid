using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    public class ApiParameterDescriptionWrapper: IApiParameterDescriptionWrapper
    {
        public string Name => this.ApiParameterDescription.Name;

        public string Source => this.ApiParameterDescription.Source.DisplayName;

        public IModelMetadataWrapper Metadata { get; }

        public IApiParameterRouteInfoWrapper RouteInfo { get; }

        [JsonIgnore]
        public ApiParameterDescription ApiParameterDescription { get; }

        public ApiParameterDescriptionWrapper(ApiParameterDescription description)
        {
            this.ApiParameterDescription = description;
            this.Metadata = new ModelMetadataWrapper(this.ApiParameterDescription.ModelMetadata);
            if (this.ApiParameterDescription.RouteInfo != null)
                this.RouteInfo = new ApiParameterRouteInfoWrapper(this.ApiParameterDescription.RouteInfo);
        }
    }
}
