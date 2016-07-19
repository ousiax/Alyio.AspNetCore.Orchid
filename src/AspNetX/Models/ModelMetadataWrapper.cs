using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AspNetX.Json.Converters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Models
{
    /// <summary>
    /// Represents a wrapper class of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata"/>.
    /// </summary>
    [DataContract]
    public class ModelMetadataWrapper
    {
        /// <summary>
        /// Gets or sets a general description of this wrapper.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the model type id of the <see cref="ModelType"/>.
        /// </summary>
        [DataMember(Name = "modelTypeId")]
        public string ModelTypeId { get; }

        /// <summary>
        /// Gets the <see cref="ModelMetadataWrapper"/> for elements of <see cref="ModelType"/> if that <see cref="System.Type"/> implements <see cref="System.Collections.IEnumerable"/>.
        /// </summary>
        [DataMember(Name = "elementMetadata")]
        public ModelMetadataWrapper ElementMetadataWrapper { get; }

        /// <summary>
        /// Gets the names and values of all <c>System.Enum</c> values in <see cref="UnderlyingOrModelType"/>.
        /// </summary>
        [DataMember(Name = "enumNamesAndValues")]
        public IReadOnlyDictionary<string, string> EnumNamesAndValues => ModelMetadata.EnumNamesAndValues;

        /// <summary>
        /// Gets a value indicating whether or not <see cref="ModelType"/> is a collection type.
        /// </summary>
        /// <remarks>
        /// A collection type is defined as a System.Type which is assignable to System.Collections.Generic.ICollection`1.
        /// </remarks>
        [DataMember(Name = "isCollectionType")]
        public bool IsCollectionType => ModelMetadata.IsCollectionType;

        /// <summary>
        /// Gets a value indicating whether Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.ModelType is a simple type.
        /// </summary>
        /// <remarks>
        /// A simple type is defined as a <see cref="System.Type"/> which has a <see cref="System.ComponentModel.TypeConverter"/> 
        /// that can convert from <see cref="System.String"/>.
        /// </remarks>
        [DataMember(Name = "isComplexType")]
        public bool IsComplexType => ModelMetadata.IsComplexType;

        /// <summary>
        /// Gets a value indicating whether <see cref="UnderlyingOrModelType"/> is for an <see cref="System.Enum"/>.
        /// </summary>
        [DataMember(Name = "isEnum")]
        public bool IsEnum => ModelMetadata.IsEnum;

        /// <summary>
        /// Gets a value indicating whether or not <see cref="ModelType"/> is an enumerable type.
        /// </summary>
        [DataMember(Name = "isEnumerableType")]
        public bool IsEnumerableType => ModelMetadata.IsEnumerableType;

        /// <summary>
        /// Gets a value indicating whether <see cref="UnderlyingOrModelType"/> is for an <see cref="System.Enum"/> with an associated <see cref="System.FlagsAttribute"/>.
        /// </summary>
        [DataMember(Name = "isFlagsEnum")]
        public bool IsFlagsEnum => ModelMetadata.IsFlagsEnum;

        /// <summary>
        /// Gets a value indicating whether or not <see cref="ModelType"/> is a <see cref="System.Nullable"/>.
        /// </summary>
        [DataMember(Name = "isNullableValueType")]
        public bool IsNullableValueType => ModelMetadata.IsNullableValueType;

        /// <summary>
        /// Gets a value indicating whether or not <see cref="ModelType"/> allows null values.
        /// </summary>
        [DataMember(Name = "isReferenceOrNullableType")]
        public bool IsReferenceOrNullableType => ModelMetadata.IsReferenceOrNullableType;

        /// <summary>
        /// Gets a value indicating the kind of metadata element represented by the current instance.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "metadataKind")]
        public ModelMetadataKind MetadataKind => ModelMetadata.MetadataKind;

        /// <summary>
        /// Gets the model type represented by the current instance.
        /// </summary>
        [JsonConverter(typeof(TypeConverter))]
        [DataMember(Name = "modelType")]
        public Type ModelType => ModelMetadata.ModelType;

        /// <summary>
        /// Gets the underlying type argument if <see cref="ModelType"/> inherits from <see cref="System.Nullable"/>. Otherwise gets <see cref="ModelType"/>.
        /// </summary>
        [JsonConverter(typeof(TypeConverter))]
        [DataMember(Name = "underlyingOrModelType")]
        public Type UnderlyingOrModelType => ModelMetadata.UnderlyingOrModelType;

        /// <summary>
        /// Gets the collection of <see cref="ModelMetadataPropertyWrapper"/> instances for the model's properties.
        /// </summary>
        [DataMember(Name = "properties")]
        public IList<ModelMetadataPropertyWrapper> Properties { get; } = new List<ModelMetadataPropertyWrapper>();

        /// <summary>
        /// Get the  metadata representation of the current instance.
        /// </summary>
        public ModelMetadata ModelMetadata { get; }

        public ModelMetadataWrapper(ModelMetadata modelMetadata)
        {
            this.ModelMetadata = modelMetadata;
            this.ModelTypeId = ModelType.GetTypeId();
        }

        public override bool Equals(object obj)
        {
            var other = obj as ModelMetadataWrapper;
            return other != null && string.Equals(other.ModelTypeId, this.ModelTypeId, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ModelTypeId?.GetHashCode() ?? 0;
        }
    }
}
