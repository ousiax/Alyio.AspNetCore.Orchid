using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AspNetX.Server.Models
{
    public class ApiModel
    {
        public IList<ApiParameterDescriptionWrapper> UriParameters { get; set; }

        public IList<ApiParameterDescriptionWrapper> BodyParameters { get; set; }

        //public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        //public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        [JsonIgnore]
        public ApiDescription ApiDescription { get; }

        public ApiModel(ApiDescription description)
        {
            this.ApiDescription = description;
            this.UriParameters = this.ApiDescription
                .ParameterDescriptions
                .Where(o => o.Source == BindingSource.Path || o.Source == BindingSource.Query)
                .Select(o => new ApiParameterDescriptionWrapper(o))
                .ToList();
            this.BodyParameters = this.ApiDescription
                .ParameterDescriptions
                .Where(o => o.Source == BindingSource.Body)
                .Select(o => new ApiParameterDescriptionWrapper(o))
                .ToList();
        }
    }
}
