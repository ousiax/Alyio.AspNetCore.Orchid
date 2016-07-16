using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Models
{
    /// <summary>
    /// Represents an API exposed by this application.
    /// </summary>
    [DataContract]
    public class ApiDescriptionModel
    {
        /// <summary>
        /// Gets or sets the URI-friendly ID for the <see cref="Microsoft.AspNetCore.Mvc.ApiExplorer.ApiDescription"/>. E.g. "Get-Values-id_name" instead of "GetValues/{id}?name={name}"
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a general description of this api.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        ///// <summary>
        ///// Gets or sets <see cref="ActionDescriptor"/> for this api.
        ///// </summary>
        //public ActionDescriptor ActionDescriptor { get; set; }

        /// <summary>
        /// Gets or sets group name for this api.
        /// </summary>
        [DataMember(Name = "groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the supported HTTP method for this api, or null if all HTTP methods are supported.
        /// </summary>
        [DataMember(Name = "httpMethod")]
        public string HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets relative url path template (relative to application root) for this api.
        /// </summary>
        [DataMember(Name = "relativePath")]
        public string RelativePath { get; set; }

        ///// <summary>
        ///// Gets a list of <see cref="ApiParameterDescription"/> for this api.
        ///// </summary>
        //public IList<ApiParameterDescription> ParameterDescriptions { get; } = new List<ApiParameterDescription>();

        ///// <summary>
        ///// Gets arbitrary metadata properties associated with the <see cref="ApiDescriptionModel"/>.
        ///// </summary>
        //public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

        ///// <summary>
        ///// Gets the list of possible formats for a response.
        ///// </summary>
        ///// <remarks>
        ///// Will be empty if the action returns no response, or if the response type is unclear. Use
        ///// <c>ProducesAttribute</c> on an action method to specify a response type.
        ///// </remarks>
        //public IList<ApiRequestFormat> SupportedRequestFormats { get; } = new List<ApiRequestFormat>();

        ///// <summary>
        ///// Gets the list of possible formats for a response.
        ///// </summary>
        ///// <remarks>
        ///// Will be empty if the action returns no response, or if the response type is unclear. Use
        ///// <c>ProducesAttribute</c> on an action method to specify a response type.
        ///// </remarks>
        //public IList<ApiResponseType> SupportedResponseTypes { get; } = new List<ApiResponseType>();

        public ApiDescription ApiDescription { get; set; }
    }
}
