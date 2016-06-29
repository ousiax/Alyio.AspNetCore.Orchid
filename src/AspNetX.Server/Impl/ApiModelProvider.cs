using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AspNetX.Server.Impl
{
    public class ApiModelProvider : IApiModelProvider
    {
        private readonly IDictionary<ApiDescription, IApiModel> _apiModelCache;
        private readonly IModelMetadataWrapperProvider _metadataWrapperProvider;

        public ApiModelProvider(IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            if (metadataWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapperProvider));
            }

            _metadataWrapperProvider = metadataWrapperProvider;
            _apiModelCache = new ConcurrentDictionary<ApiDescription, IApiModel>();
        }

        public IApiModel GetApiModel(ApiDescription description)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            IApiModel apiModel;
            if (!_apiModelCache.TryGetValue(description, out apiModel))
            {
                apiModel = new ApiModel(description, _metadataWrapperProvider);
                _apiModelCache.Add(description, apiModel);
            }
            return apiModel;
        }

        #region ApiModel

        internal class ApiModel : IApiModel
        {
            public IList<IApiParameterDescriptionWrapper> UriParameters { get; set; }

            public IList<IApiParameterDescriptionWrapper> BodyParameters { get; set; }

            //public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

            //public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

            [JsonIgnore]
            public ApiDescription ApiDescription { get; }

            public ApiModel(
                ApiDescription description,
                IModelMetadataWrapperProvider metadataWrapperProvider)
            {
                this.ApiDescription = description;
                this.UriParameters = this.ApiDescription
                    .ParameterDescriptions
                    .Where(o => o.Source == BindingSource.Path || o.Source == BindingSource.Query)
                    .Select(o => new ApiParameterDescriptionWrapper(o, metadataWrapperProvider))
                    .ToList<IApiParameterDescriptionWrapper>();
                this.BodyParameters = this.ApiDescription
                    .ParameterDescriptions
                    .Where(o => o.Source == BindingSource.Body)
                    .Select(o => new ApiParameterDescriptionWrapper(o, metadataWrapperProvider))
                    .ToList<IApiParameterDescriptionWrapper>();
            }
        }

        #endregion
    }
}
