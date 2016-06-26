using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.ViewModels
{
    /// <summary>
    /// Represents an API exposed by this application.
    /// </summary>
    public class ApiXDescription
    {
        [JsonIgnore]
        public ApiDescription ApiDescription { get; }

        public ApiXDescription(ApiDescription apiDescription)
        {
            this.ApiDescription = apiDescription;
        }

        public string Id { get; set; }

        /// <summary>
        /// The group name for this api.
        /// </summary>
        public string GroupName { get { return this.ApiDescription.GroupName; } set { this.ApiDescription.GroupName = value; } }

        /// <summary>
        /// The supported HTTP method for this api, or null if all HTTP methods are supported.
        /// </summary>
        public string HttpMethod { get { return this.ApiDescription.HttpMethod; } set { this.ApiDescription.HttpMethod = value; } }

        /// <summary>
        /// The relative url path template (relative to application root) for this api.
        /// </summary>
        public string RelativePath { get { return this.ApiDescription.RelativePath; } set { this.ApiDescription.RelativePath = value; } }
    }
}