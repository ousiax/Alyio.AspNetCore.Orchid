using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspNetX.Services
{
    /// <inheritdoc />
    public class ApiDescriptionDetailModelProvider : IApiDescriptionDetailModelProvider
    {
        private readonly IReadOnlyDictionary<string, ApiDescriptionDetailModel> _apiDescriptionDetailModelCache;

        private readonly IModelMetadataWrapperProvider _modelMetadataWrapperProvider;
        private readonly IObjectGenerator _objectGenerator;
        private readonly IDocumentationProvider _documentationProvider;
        private readonly ILogger _logger;

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
            IDocumentationProviderFactory documentationProviderFactory,
            ILoggerFactory loggerFactory)
        {
            _modelMetadataWrapperProvider = modelMetadataWrapperProvider;
            _objectGenerator = objectGenerator;
            _documentationProvider = documentationProviderFactory.Create();
            _logger = loggerFactory.CreateLogger("ASPNETX");

            var apiDescriptionGroupModels = apiDescriptionGroupModelCollectionProvider
                .ApiDescriptionGroups
                .Items
                .SelectMany(g => g.Items)
                .ToList();
            foreach (var item in apiDescriptionGroupModels.GroupBy(o => o.Id).Where(p => p.Count() > 1))
            {
                var controllerActionDescriptors = item.Select(o => o.ApiDescription.ActionDescriptor)
                    .OfType<ControllerActionDescriptor>()
                    .Select(p => new JObject
                    {
                        { "ControllerName", p.ControllerName},
                        { "ActionName", p.ActionName},
                        { "RouteTemplate", p.AttributeRouteInfo.Template},
                    });
                _logger.LogWarning($"dumplicate web apis with the same route template.{Environment.NewLine}{JsonConvert.SerializeObject(controllerActionDescriptors, Formatting.Indented)}");
            }
            var dictionary = apiDescriptionGroupModels
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
                RelativePath = apiDescriptionModel.RelativePath,
                ApiDescription = apiDescriptionModel.ApiDescription
            };
            BuildRequestInformation(apiDescriptionDetailModel);
            BuildResponseInformation(apiDescriptionDetailModel);
            return apiDescriptionDetailModel;
        }

        private void BuildResponseInformation(ApiDescriptionDetailModel apiDescriptionDetailModel)
        {
            foreach (var supportedResponseType in apiDescriptionDetailModel.ApiDescription.SupportedResponseTypes)
            {
                apiDescriptionDetailModel.ResponseInformation.SupportedResponseTypes.Add(supportedResponseType);
                if (supportedResponseType.Type != null)
                {
                    var modelMetadataWrapper = _modelMetadataWrapperProvider.GetOrCreate(supportedResponseType.Type);
                    apiDescriptionDetailModel.ResponseInformation.SupportedResponseTypeMetadatas.Add(modelMetadataWrapper);
                    apiDescriptionDetailModel.ResponseInformation.SupportedResponseSamples.Add("application/json", _objectGenerator.GenerateObject(supportedResponseType.Type));
                }
            }
        }

        private void BuildRequestInformation(ApiDescriptionDetailModel apiDescriptionDetailModel)
        {
            IDictionary<string, string> parameterDescriptions = new Dictionary<string, string>();
            var controllerActionDescriptor = apiDescriptionDetailModel.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (_documentationProvider != null && controllerActionDescriptor != null)
            {
                parameterDescriptions = _documentationProvider.GetParameterDocumentation(controllerActionDescriptor.MethodInfo);
            }

            foreach (var parameter in apiDescriptionDetailModel.ApiDescription.ParameterDescriptions)
            {
                if (parameter.Source == BindingSource.Body)
                {
                    apiDescriptionDetailModel.RequestInformation.BodyParameterDescriptions.Add(CreateApiParameterDescriptionModel(parameter, parameterDescriptions));
                    apiDescriptionDetailModel.RequestInformation.SupportedRequestSamples.Add("application/json", _objectGenerator.GenerateObject(parameter.Type));
                }
                else if (parameter.Source == BindingSource.Query || parameter.Source == BindingSource.Path)
                {
                    apiDescriptionDetailModel.RequestInformation.UriParameterDescriptions.Add(CreateApiParameterDescriptionModel(parameter, parameterDescriptions));
                }
            }
        }

        private ApiParameterDescriptionModel CreateApiParameterDescriptionModel(ApiParameterDescription parameter, IDictionary<string, string> descriptions)
        {
            var apiParameterDescriptionModel = new ApiParameterDescriptionModel
            {
                ApiParameterDescription = parameter,
            };
            if (parameter.ModelMetadata != null)
            {
                apiParameterDescriptionModel.Metadata = _modelMetadataWrapperProvider.GetOrCreate(parameter.ModelMetadata.ModelType);
                string desc = string.Empty;
                descriptions.TryGetValue(parameter.Name, out desc);
                apiParameterDescriptionModel.Description = desc;
            }
            return apiParameterDescriptionModel;
        }

        IEnumerator IEnumerable.GetEnumerator() => _apiDescriptionDetailModelCache.GetEnumerator();
    }
}
