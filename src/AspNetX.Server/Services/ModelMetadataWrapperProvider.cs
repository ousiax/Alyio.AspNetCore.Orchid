using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNet.Mvc.ModelBinding;

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

        /// <inheritdoc />
        public bool TryAdd(Type modelType, out ModelMetadataWrapper modelMetadataWrapper)
        {
            string modelTypeId = modelType.GetTypeId();
            bool result = TryUpdateModelTypeCache(modelTypeId, modelType);

            TryGetOrCreateModelMetadataWrapperCache(modelTypeId, out modelMetadataWrapper);

            return result;
        }

        /// <inheritdoc />
        public bool TryGet(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper)
        {
            if (_modelTypeCache.ContainsKey(modelTypeId))
            {
                TryGetOrCreateModelMetadataWrapperCache(modelTypeId, out modelMetadataWrapper);
                return true;
            }
            modelMetadataWrapper = null;
            return false;
        }

        /// <summary>
        /// Try to update model metedata wrapper cache.
        /// </summary>
        /// <param name="modelTypeId">The type id of the model type.</param>
        /// <param name="modelMetadataWrapper">The metadata wrapper of the model type's metadata.</param>
        /// <returns>True if updated, otherwise false.</returns>
        private bool TryGetOrCreateModelMetadataWrapperCache(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper)
        {
            Type modelType = _modelTypeCache[modelTypeId];
            if (!_modelMetadataWrapperCache.ContainsKey(modelTypeId))
            {
                ModelMetadata modelMetadata = _modelMetadataProvider.GetMetadataForType(modelType);

                modelMetadataWrapper = new ModelMetadataWrapper(modelMetadata, this)
                {
                    Description = _documentationProvider?.GetDocumentation(modelType)
                };
                _modelMetadataWrapperCache.Add(modelTypeId, modelMetadataWrapper);

                var modelTypeInfo = modelType.GetTypeInfo();
                foreach (var property in modelMetadata.Properties)
                {
                    var propertyModelTypId = property.ModelType.GetTypeId();

                    TryUpdateModelTypeCache(propertyModelTypId, property.ModelType);

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

        private bool TryUpdateModelTypeCache(string modelTypeId, Type modelType)
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
