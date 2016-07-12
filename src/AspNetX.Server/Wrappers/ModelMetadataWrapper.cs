using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Converters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Server.Wrappers
{
    [DebuggerDisplay("ModelType = {ModelType}")]
    internal class ModelMetadataWrapper : IModelMetadataWrapper
    {
        public int Id { get; }

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ContainerType => Metadata.ContainerType;

        public IModelMetadataWrapper ElementMetadataWrapper { get; }

        public IReadOnlyDictionary<string, string> EnumNamesAndValues => Metadata.EnumNamesAndValues;

        public bool IsCollectionType => Metadata.IsCollectionType;

        public bool IsComplexType => Metadata.IsComplexType;

        public bool IsEnum => Metadata.IsEnum;

        public bool IsEnumerableType => Metadata.IsEnumerableType;

        public bool IsFlagsEnum => Metadata.IsFlagsEnum;

        public bool IsNullableValueType => Metadata.IsNullableValueType;

        public bool IsReadOnly => Metadata.IsReadOnly;

        public bool IsReferenceOrNullableType => Metadata.IsReferenceOrNullableType;

        public bool IsRequired => Metadata.IsRequired;

        [JsonConverter(typeof(StringEnumConverter))]
        public ModelMetadataKind MetadataKind => Metadata.MetadataKind;

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ModelType => Metadata.ModelType;

        public string PropertyName => Metadata.PropertyName;

        public IReadOnlyCollection<IModelPropertyWrapper> Properties { get; }

        [JsonIgnore]
        public ModelMetadata Metadata { get; }

        [JsonIgnore]
        public ModelMetadata ElementMetadata => Metadata.ElementMetadata;

        public ModelMetadataWrapper(
            ModelMetadata metadata,
            IModelMetadataIdentityProvider identityProvider,
            IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            if (identityProvider == null)
            {
                throw new ArgumentNullException(nameof(identityProvider));
            }
            if (metadataWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapperProvider));
            }

            this.Metadata = metadata;
            this.Id = identityProvider.GetId(metadata.ToMetadataIdentity());

            metadataWrapperProvider.TryAdd(this); // Important!, terminal recursion loops.

            if (this.IsComplexType && !(this.IsCollectionType || this.IsEnumerableType))
            {
                this.Properties = metadata.Properties?.Select(o => metadataWrapperProvider.GetModelPropertyWrapper(o)).ToList().AsReadOnly();
            }
            if (this.ElementMetadata != null)
            {
                this.ElementMetadataWrapper = metadataWrapperProvider.GetModelMetadataWrapper(ElementMetadata);
            }
        }
    }
}
