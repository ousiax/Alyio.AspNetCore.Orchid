using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetX.Services
{
    /// <inheritdoc />
    public class ApiDescriptionDetailModelProvider : IApiDescriptionDetailModelProvider
    {
        private readonly IReadOnlyDictionary<string, ApiDescriptionDetailModel> _apiDescriptionDetailModelCache;

        private readonly IModelMetadataWrapperProvider _modelMetadataWrapperProvider;
        private readonly IObjectGenerator _objectGenerator;
        private readonly IDocumentationProvider _documentationProvider;

        /// <summary>
        /// Creates a new instance of <see cref="ApiDescriptionDetailModelProvider"/>.
        /// </summary>
        /// <param name="modelMetadataWrapperProvider">
        /// The <see cref="IModelMetadataWrapperProvider"/>.
        /// </param>
        /// <param name="apiDescriptionGroupModelCollectionProvider">
        /// The <see cref="IApiDescriptionGroupModelCollectionProvider"/>.
        /// </param>
        public ApiDescriptionDetailModelProvider(
            IModelMetadataWrapperProvider modelMetadataWrapperProvider,
            IApiDescriptionGroupModelCollectionProvider apiDescriptionGroupModelCollectionProvider,
            IObjectGenerator objectGenerator,
            IDocumentationProviderFactory documentationProviderFactory)
        {
            _modelMetadataWrapperProvider = modelMetadataWrapperProvider;
            _objectGenerator = objectGenerator;
            _documentationProvider = documentationProviderFactory.Create();

            var dictionary = apiDescriptionGroupModelCollectionProvider
                .ApiDescriptionGroups
                .Items
                .SelectMany(g => g.Items)
                .Distinct()
                .ToDictionary(o => o.Id, o => CreateApiDescriptionDetailModel(o));   //TODO Maybe exits dumpliate ids.
            _apiDescriptionDetailModelCache = new ReadOnlyDictionary<string, ApiDescriptionDetailModel>(dictionary);
        }

        public ApiDescriptionDetailModel this[string key] => _apiDescriptionDetailModelCache[key];

        public int Count => _apiDescriptionDetailModelCache.Count;

        public IEnumerable<string> Keys => _apiDescriptionDetailModelCache.Keys;

        public IEnumerable<ApiDescriptionDetailModel> Values => _apiDescriptionDetailModelCache.Values;

        public bool ContainsKey(string key) => _apiDescriptionDetailModelCache.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, ApiDescriptionDetailModel>> GetEnumerator() => _apiDescriptionDetailModelCache.GetEnumerator();

        public bool TryGetValue(string key, out ApiDescriptionDetailModel value) => _apiDescriptionDetailModelCache.TryGetValue(key, out value);

        private ApiDescriptionDetailModel CreateApiDescriptionDetailModel(ApiDescriptionModel apiDescriptionModel)
        {
            var apiDescriptionDetailModel = new ApiDescriptionDetailModel
            {
                Id = apiDescriptionModel.Id,
                Description = apiDescriptionModel.Description,
                GroupName = apiDescriptionModel.GroupName,
                HttpMethod = apiDescriptionModel.HttpMethod,
                RelativePath = apiDescriptionModel.RelativePath
            };
            var apiDescription = apiDescriptionModel.ApiDescription;
            foreach (var parameter in apiDescription.ParameterDescriptions)
            {
                if (parameter.Source == BindingSource.Body)
                {
                    apiDescriptionDetailModel.RequestInformation.BodyParameterDescriptions.Add(CreateApiParameterDescriptionModel(parameter));
                    apiDescriptionDetailModel.RequestInformation.SupportedRequestSamples.Add("application/json", _objectGenerator.GenerateObject(parameter.Type));
                }
                else if (parameter.Source == BindingSource.Query || parameter.Source == BindingSource.Path)
                {
                    apiDescriptionDetailModel.RequestInformation.UriParameterDescriptions.Add(CreateApiParameterDescriptionModel(parameter));
                }
            }
            foreach (var supportedResponseType in apiDescription.SupportedResponseTypes)
            {
                apiDescriptionDetailModel.ResponseInformation.SupportedResponseTypes.Add(supportedResponseType);
                if (supportedResponseType.Type != null)
                {
                    var modelMetadataWrapper = _modelMetadataWrapperProvider.GetOrCreate(supportedResponseType.Type);
                    apiDescriptionDetailModel.ResponseInformation.SupportedResponseTypeMetadatas.Add(modelMetadataWrapper);
                    apiDescriptionDetailModel.ResponseInformation.SupportedResponseSamples.Add("application/json", _objectGenerator.GenerateObject(supportedResponseType.Type));
                }
            }
            return apiDescriptionDetailModel;
        }

        private ApiParameterDescriptionModel CreateApiParameterDescriptionModel(ApiParameterDescription parameter)
        {
            var apiParameterDescriptionModel = new ApiParameterDescriptionModel
            {
                ApiParameterDescription = parameter,
            };
            if (parameter.ModelMetadata != null)
            {
                apiParameterDescriptionModel.Metadata = _modelMetadataWrapperProvider.GetOrCreate(parameter.ModelMetadata.ModelType);
                apiParameterDescriptionModel.Description = _documentationProvider?.GetDocumentation(parameter.ModelMetadata.ModelType); //TODO Get parameter descriptino from action method.
            }
            return apiParameterDescriptionModel;
        }

        IEnumerator IEnumerable.GetEnumerator() => _apiDescriptionDetailModelCache.GetEnumerator();
    }
}
