using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Abstractions
{
    public interface IApiModel
    {
        IList<IApiParameterDescriptionWrapper> UriParameters { get; set; }

        IList<IApiParameterDescriptionWrapper> BodyParameters { get; set; }

        // IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        // IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        ApiDescription ApiDescription { get; }
    }
}
