using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Server.Wrappers
{
    public class ModelMetadataWrapper : IModelMetadataWrapper
    {
        public string ContainerType => Metadata.ContainerType?.GetTypeName();

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

        public string ModelType => Metadata.ModelType.GetTypeName();

        public string PropertyName => Metadata.PropertyName;

        [JsonIgnore]
        public ModelMetadata Metadata { get; }

        public ModelMetadataWrapper(ModelMetadata metadata)
        {
            this.Metadata = metadata;
        }
    }
}
