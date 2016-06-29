using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.Net.Http.Headers;

namespace AspNetX.Server.Abstractions
{
    public interface IApiModel
    {
        IReadOnlyCollection<IApiParameterDescriptionWrapper> UriParameters { get; }

        IReadOnlyCollection<IApiParameterDescriptionWrapper> BodyParameters { get; }

        IReadOnlyDictionary<MediaTypeHeaderValue, object> SampleRequests { get; }

        IReadOnlyDictionary<MediaTypeHeaderValue, object> SampleResponses { get; }

        ApiDescription ApiDescription { get; }
    }
}
