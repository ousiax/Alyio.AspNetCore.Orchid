using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace AspNetX.Server.Impl
{
    public class ApiModelProvider : IApiModelProvider
    {
        private readonly IDictionary<ApiDescription, IApiModel> _apiModelCache;
        private readonly IModelMetadataWrapperProvider _metadataWrapperProvider;
        private readonly IObjectGenerator _objectGenerator;
        private readonly IServiceProvider _serviceProvider;

        public ApiModelProvider(IModelMetadataWrapperProvider metadataWrapperProvider, IObjectGenerator objectGenerator, IServiceProvider serviceProvider)
        {
            if (metadataWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapperProvider));
            }
            if (objectGenerator == null)
            {
                throw new ArgumentNullException(nameof(objectGenerator));
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _metadataWrapperProvider = metadataWrapperProvider;
            _objectGenerator = objectGenerator;
            _serviceProvider = serviceProvider;

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
                apiModel = new ApiModel(description, _metadataWrapperProvider, _objectGenerator) { ServiceProvider = _serviceProvider };
                _apiModelCache.Add(description, apiModel);
            }
            return apiModel;
        }

        #region ApiModel

        internal sealed class ApiModel : IApiModel
        {
            private IModelMetadataWrapper _responseModelMetadataWrapper;

            [JsonIgnore]
            public IServiceProvider ServiceProvider { get; set; }

            public IReadOnlyCollection<IApiParameterDescriptionWrapper> UriParameters { get; }

            public IApiParameterDescriptionWrapper BodyParameter { get; }

            public IReadOnlyDictionary<MediaTypeHeaderValue, object> SampleRequests { get; }

            public IReadOnlyDictionary<MediaTypeHeaderValue, object> SampleResponses { get; }

            [JsonIgnore]
            public ApiDescription ApiDescription { get; }

            public IModelMetadataWrapper ResponseModelMetadataWrapper
            {
                get
                {
                    string name = ApiDescription.ResponseModelMetadata?.ModelType.Name;

                    if (_responseModelMetadataWrapper == null && ApiDescription.ResponseModelMetadata != null)
                    {
                        _responseModelMetadataWrapper = ServiceProvider.GetService<IModelMetadataWrapperProvider>().GetModelMetadataWrapper(ApiDescription.ResponseModelMetadata);
                    }
                    return _responseModelMetadataWrapper;
                }
            }

            public ApiModel(
                ApiDescription description,
                IModelMetadataWrapperProvider metadataWrapperProvider,
                IObjectGenerator objectGenerator)
            {
                this.ApiDescription = description;
                this.UriParameters = this.ApiDescription
                    .ParameterDescriptions
                    .Where(o => o.Source == BindingSource.Path || o.Source == BindingSource.Query)
                    .Select(o => new ApiParameterDescriptionWrapper(o, metadataWrapperProvider))
                    .ToList<IApiParameterDescriptionWrapper>()
                    .AsReadOnly();
                this.BodyParameter = this.ApiDescription
                    .ParameterDescriptions
                    .Where(o => o.Source == BindingSource.Body)
                    .Select(o => new ApiParameterDescriptionWrapper(o, metadataWrapperProvider))
                    .FirstOrDefault();
                if (this.BodyParameter != null)
                {
                    this.SampleRequests = new ReadOnlyDictionary<MediaTypeHeaderValue, object>(
                        new Dictionary<MediaTypeHeaderValue, object>
                        {
                        { new MediaTypeHeaderValue("application/json"), objectGenerator.GenerateObject(BodyParameter.MetadataWrapper.ModelType) }
                        });
                }
                if (this.ApiDescription.ResponseType != null)
                {
                    this.SampleResponses = new ReadOnlyDictionary<MediaTypeHeaderValue, object>(
                        new Dictionary<MediaTypeHeaderValue, object>
                        {
                        { new MediaTypeHeaderValue("application/json"), objectGenerator.GenerateObject(this.ApiDescription.ResponseType) }
                        });
                }
            }
        }

        #endregion
    }
}
