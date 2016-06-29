using System;
using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Converters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Server.Wrappers
{
    internal class ModelMetadataWrapper : IModelMetadataWrapper
    {
        public int Id { get; }

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ContainerType => Metadata.ContainerType;

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

        public IReadOnlyCollection<ModelPropertyWrapper> Properties { get; }

        [JsonIgnore]
        public ModelMetadata Metadata { get; }

        public ModelMetadataWrapper(ModelMetadata metadata, IModelMetadataIdentityProvider identityProvider)
        {
            this.Metadata = metadata;
            if (MetadataKind == ModelMetadataKind.Type)
            {
                this.Id = identityProvider.GetId(ModelMetadataIdentity.ForType(ModelType));
            }
            else
            {
                this.Id = identityProvider.GetId(ModelMetadataIdentity.ForProperty(ModelType, PropertyName, ContainerType));
            }
            this.Properties = metadata.Properties?.Select(o => new ModelPropertyWrapper(o, identityProvider)).ToList().AsReadOnly();
        }
    }
}
