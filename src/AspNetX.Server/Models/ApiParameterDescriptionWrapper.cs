using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Models
{
    public class ApiParameterDescriptionWrapper
    {
        public string Name => this.ApiParameterDescription.Name;

        public string Type => this.ApiParameterDescription.Type.GetTypeName();

        [JsonIgnore]
        public ApiParameterDescription ApiParameterDescription { get; }

        public ApiParameterDescriptionWrapper(ApiParameterDescription description)
        {
            this.ApiParameterDescription = description;
        }
    }
}
