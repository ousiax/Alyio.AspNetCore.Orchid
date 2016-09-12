using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace AspNetX.Models
{
    /// <summary>
    /// Represents an API detail exposed by this application.
    /// </summary>
    [DataContract]
    public class ApiDescriptionDetailModel : ApiDescriptionModel
    {
        /// <summary>
        /// Gets the <see cref="RequestInformation"/>.
        /// </summary>
        [DataMember(Name = "requestInformation", Order = 1000)]
        public RequestInformation RequestInformation { get; } = new RequestInformation();

        /// <summary>
        /// Gets the <see cref="ResponseInformation"/>.
        /// </summary>
        [DataMember(Name = "responseInformation", Order = 1001)]
        public ResponseInformation ResponseInformation { get; } = new ResponseInformation();

        public override bool Equals(object obj)
        {
            var other = obj as ApiDescriptionDetailModel;
            if (other != null)
            {
                return string.Equals(this.Id, other.Id, StringComparison.OrdinalIgnoreCase);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? 0;
        }
    }

    /// <summary>
    /// Represents the request information of an API.
    /// </summary>
    [DataContract]
    public class RequestInformation
    {
        /// <summary>
        /// Gets a list of Path parameters <see cref="ApiParameterDescriptionModel"/> for this api.
        /// </summary>
        [DataMember(Name = "pathParameterDescriptions")]
        public IList<ApiParameterDescriptionModel> RouteParameterDescriptions { get; } = new List<ApiParameterDescriptionModel>();

        /// <summary>
        /// Gets a list of Query parameters <see cref="ApiParameterDescriptionModel"/> for this api.
        /// </summary>
        [DataMember(Name = "queryParameterDescriptions")]
        public IList<ApiParameterDescriptionModel> QueryParameterDescriptions { get; } = new List<ApiParameterDescriptionModel>();

        /// <summary>
        /// Gets a list of body <see cref="ApiParameterDescriptionModel"/> for this api.
        /// </summary>
        [DataMember(Name = "bodyParameterDescriptions")]
        public IList<ApiParameterDescriptionModel> BodyParameterDescriptions { get; } = new List<ApiParameterDescriptionModel>();

        /// <summary>
        /// Gets the list of possible formats for a response.
        /// </summary>
        /// <remarks>
        /// Will be empty if the action returns no response, or if the response type is unclear. Use
        /// <c>ProducesAttribute</c> on an action method to specify a response type.
        /// </remarks>
        //[DataMember(Name = "supportedRequestFormats")]
        public IList<ApiRequestFormat> SupportedRequestFormats { get; } = new List<ApiRequestFormat>();

        [DataMember(Name = "supportedRequestSamples")]
        public IDictionary<string, object> SupportedRequestSamples { get; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// Represents the response information of an API.
    /// </summary>
    [DataContract]
    public class ResponseInformation
    {
        /// <summary>
        /// Gets the list of possible formats for a response.
        /// </summary>
        /// <remarks>
        /// Will be empty if the action returns no response, or if the response type is unclear. Use
        /// <c>ProducesAttribute</c> on an action method to specify a response type.
        /// </remarks>
        //[DataMember(Name = "supportedResponseTypes")]
        public IList<ApiResponseType> SupportedResponseTypes { get; } = new List<ApiResponseType>();

        [DataMember(Name = "supportedResponseTypeMetadatas")]
        public IList<ModelMetadataWrapper> SupportedResponseTypeMetadatas { get; } = new List<ModelMetadataWrapper>();

        [DataMember(Name = "supportedResponseSamples")]
        public IDictionary<string, object> SupportedResponseSamples { get; } = new Dictionary<string, object>();
    }
}
