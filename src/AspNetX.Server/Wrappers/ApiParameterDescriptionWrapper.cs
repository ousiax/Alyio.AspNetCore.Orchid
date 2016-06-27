using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    public class ApiParameterDescriptionWrapper
    {
        public string Name => this.ApiParameterDescription.Name;

        public string Type => this.ApiParameterDescription.Type.GetTypeName();

        public string Source => this.ApiParameterDescription.Source.DisplayName;

        public ApiParameterRouteInfoWrapper RouteInfo { get; }

        [JsonIgnore]
        public ApiParameterDescription ApiParameterDescription { get; }

        public ApiParameterDescriptionWrapper(ApiParameterDescription description)
        {
            this.ApiParameterDescription = description;
            if (this.ApiParameterDescription.RouteInfo != null)
                this.RouteInfo = new ApiParameterRouteInfoWrapper(this.ApiParameterDescription.RouteInfo);
        }
    }
}
