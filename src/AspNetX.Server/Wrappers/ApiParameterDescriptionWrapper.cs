using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    internal class ApiParameterDescriptionWrapper : IApiParameterDescriptionWrapper
    {
        public string Name => this.ApiParameterDescription.Name;

        public string Source => this.ApiParameterDescription.Source.DisplayName;

        public IModelMetadataWrapper MetadataWrapper { get; }

        public IApiParameterRouteInfoWrapper RouteInfo { get; }

        [JsonIgnore]
        public ApiParameterDescription ApiParameterDescription { get; }

        public ApiParameterDescriptionWrapper(
            ApiParameterDescription description,
            IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            this.ApiParameterDescription = description;
            if (this.ApiParameterDescription.ModelMetadata != null) //TODO ApiParameterDescription.ModelMetadata is NULL ?
                this.MetadataWrapper = metadataWrapperProvider.GetModelMetadataWrapper(this.ApiParameterDescription.ModelMetadata);
            if (this.ApiParameterDescription.RouteInfo != null)
                this.RouteInfo = new ApiParameterRouteInfoWrapper(this.ApiParameterDescription.RouteInfo);
        }
    }
}
