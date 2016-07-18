using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetX.Services
{
    /// <inheritdoc />
    public class ModelMetadataWrapperProvider : IModelMetadataWrapperProvider
    {
        private readonly IDictionary<string, ModelMetadataWrapper> _modelMetadataWrapperCache = new ConcurrentDictionary<string, ModelMetadataWrapper>();
        private readonly IDictionary<string, Type> _modelTypeCache = new ConcurrentDictionary<string, Type>();

        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IDocumentationProvider _documentationProvider;

        public ModelMetadataWrapperProvider(IModelMetadataProvider modelMetadataProvider, IDocumentationProviderFactory documentationProviderFactory)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _documentationProvider = documentationProviderFactory.Create();
        }

        /// <inheritdoc />
        public bool TryAdd(Type modelType, out ModelMetadataWrapper modelMetadataWrapper)
        {
            string modelTypeId = modelType.GetTypeId();
            bool result = TryAddModelTypeCache(modelTypeId, modelType);

            TryUpdateModelMetadataWrapperCache(modelTypeId, out modelMetadataWrapper);

            return result;
        }

        /// <inheritdoc />
        public bool TryGet(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper)
        {
            if (_modelTypeCache.ContainsKey(modelTypeId))
            {
                return TryUpdateModelMetadataWrapperCache(modelTypeId, out modelMetadataWrapper);
            }
            modelMetadataWrapper = null;
            return false;
        }

        //TODO adjust the method's algorithm.
        private bool TryUpdateModelMetadataWrapperCache(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper)
        {
            Type modelType = _modelTypeCache[modelTypeId];
            if (!_modelMetadataWrapperCache.ContainsKey(modelTypeId))
            {
                ModelMetadata modelMetadata = _modelMetadataProvider.GetMetadataForType(modelType);

                modelMetadataWrapper = new ModelMetadataWrapper(modelMetadata)
                {
                    Description = _documentationProvider?.GetDocumentation(modelType)
                };
                _modelMetadataWrapperCache.Add(modelTypeId, modelMetadataWrapper);

                var modelTypeInfo = modelType.GetTypeInfo();
                foreach (var property in modelMetadata.Properties)
                {
                    var propertyModelTypId = property.ModelType.GetTypeId();

                    TryAddModelTypeCache(propertyModelTypId, property.ModelType);

                    var propertyInfo = modelTypeInfo.GetProperty(property.PropertyName);
                    var propertyWrapper = new ModelMetadataPropertyWrapper(property)
                    {
                        Description = _documentationProvider?.GetDocumentation(propertyInfo)
                    };
                    modelMetadataWrapper.Properties.Add(propertyWrapper);
                }
                return true;
            }
            else
            {
                modelMetadataWrapper = _modelMetadataWrapperCache[modelTypeId];
                return false;
            }
        }

        private bool TryAddModelTypeCache(string modelTypeId, Type modelType)
        {
            if (!_modelTypeCache.ContainsKey(modelTypeId))
            {
                _modelTypeCache.Add(modelTypeId, modelType);
                return true;
            }
            return false;
        }
    }
}
