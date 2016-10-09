using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DoqX.Abstractions;
using DoqX.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DoqX.Services
{
    /// <inheritdoc />
    [DebuggerDisplay("ModelType = {_modelTypeCache.Count}")]
    public class ModelMetadataWrapperProvider : IModelMetadataWrapperProvider
    {
        private readonly IDictionary<string, ModelMetadataWrapper> _modelMetadataWrapperCache = new ConcurrentDictionary<string, ModelMetadataWrapper>();
        private readonly IDictionary<string, Type> _modelTypeCache = new ConcurrentDictionary<string, Type>();

        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly IDocumentationProvider _documentationProvider;

        public ModelMetadataWrapperProvider(IModelMetadataProvider modelMetadataProvider, IDocumentationProvider documentationProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
            _documentationProvider = documentationProvider;
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
                    Description = _documentationProvider.GetDocumentation(modelType.GetTypeInfo())
                };

                var modelTypeInfo = modelType.GetTypeInfo();
                foreach (var property in modelMetadata.Properties)
                {
                    var propertyInfo = modelTypeInfo.GetProperty(property.PropertyName);
                    var propertyWrapper = new ModelMetadataPropertyWrapper(property, this)
                    {
                        Description = _documentationProvider.GetDocumentation(propertyInfo)
                    };
                    modelMetadataWrapper.Properties.Add(propertyWrapper);
                }
                if (modelMetadata.IsEnum)
                {
                    foreach (var enumNameAndValue in modelMetadata.EnumNamesAndValues)
                    {
                        var enumFieldDescription = new EnumFieldDescription
                        {
                            Name = enumNameAndValue.Key,
                            Value = enumNameAndValue.Value,
                            Description = _documentationProvider.GetDocumentation(modelTypeInfo.GetField(enumNameAndValue.Key))
                        };
                        modelMetadataWrapper.EnumFieldDescrptions.Add(enumFieldDescription);
                    }
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
