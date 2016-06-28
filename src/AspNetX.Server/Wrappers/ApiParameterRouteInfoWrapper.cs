using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    public class ApiParameterRouteInfoWrapper: IApiParameterRouteInfoWrapper
    {
        public object DefaultValue => this.ApiParameterRouteInfo?.DefaultValue;

        public bool? IsOptional => this.ApiParameterRouteInfo?.IsOptional;

        [JsonIgnore]
        public ApiParameterRouteInfo ApiParameterRouteInfo { get; }

        public ApiParameterRouteInfoWrapper(ApiParameterRouteInfo routeInfo)
        {
            this.ApiParameterRouteInfo = routeInfo;
        }
    }
}
