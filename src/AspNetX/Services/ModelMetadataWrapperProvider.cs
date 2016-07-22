using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetX.Services
{
    /// <inheritdoc />
    [DebuggerDisplay("ModelType = {_modelTypeCache.Count}")]
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

        public void RegisterModelType(Type modelType)
        {
            var modelTypeId = modelType.GetTypeId();
            if (!_modelTypeCache.ContainsKey(modelTypeId))
            {
                _modelTypeCache[modelTypeId] = modelType;
            }
        }

        public void RegisterModelMetadataWrapper(ModelMetadataWrapper modelMetadataWrapper)
        {
            if (!_modelMetadataWrapperCache.ContainsKey(modelMetadataWrapper.ModelTypeId))
            {
                _modelMetadataWrapperCache[modelMetadataWrapper.ModelTypeId] = modelMetadataWrapper;
            }
        }

        public ModelMetadataWrapper GetOrCreate(Type modelType)
        {
            ModelMetadataWrapper modelMetadataWrapper;
            var modelTypeId = modelType.GetTypeId();
            if (_modelMetadataWrapperCache.ContainsKey(modelTypeId))
            {
                modelMetadataWrapper = _modelMetadataWrapperCache[modelTypeId];
            }
            else
            {
                ModelMetadata modelMetadata = _modelMetadataProvider.GetMetadataForType(modelType);

                modelMetadataWrapper = new ModelMetadataWrapper(modelMetadata, this)
                {
                    Description = _documentationProvider?.GetDocumentation(modelType)
                };

                var modelTypeInfo = modelType.GetTypeInfo();
                foreach (var property in modelMetadata.Properties)
                {
                    var propertyInfo = modelTypeInfo.GetProperty(property.PropertyName);
                    var propertyWrapper = new ModelMetadataPropertyWrapper(property, this)
                    {
                        Description = _documentationProvider?.GetDocumentation(propertyInfo)
                    };
                    modelMetadataWrapper.Properties.Add(propertyWrapper);
                }
            }
            return modelMetadataWrapper;
        }

        /// <inheritdoc />
        public bool TryGet(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper)
        {
            if (_modelTypeCache.ContainsKey(modelTypeId))
            {
                Type modelType = _modelTypeCache[modelTypeId];
                modelMetadataWrapper = GetOrCreate(modelType);
                return true;
            }
            modelMetadataWrapper = null;
            return false;
        }
    }
}
