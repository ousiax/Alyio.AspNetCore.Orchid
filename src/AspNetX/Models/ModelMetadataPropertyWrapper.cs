using System;
using System.Runtime.Serialization;
using AspNetX.Abstractions;
using AspNetX.Json.Converters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Models
{
    /// <summary>
    /// Represents a wrapper class of <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.Properties"/>.
    /// </summary>
    [DataContract]
    public class ModelMetadataPropertyWrapper
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
        /// Gets the container type of this metadata if it represents a property, otherwise null.
        /// </summary>
        [JsonConverter(typeof(TypeConverter))]
        [DataMember(Name = "containerType")]
        public Type ContainerType => ModelMetadata.ContainerType;

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
        /// Gets the property name represented by the current instance.
        /// </summary>
        [DataMember(Name = "propertyName")]
        public string PropertyName => ModelMetadata.PropertyName;

        /// <summary>
        /// Get the  metadata representation of the current instance.
        /// </summary>
        public ModelMetadata ModelMetadata { get; }

        public ModelMetadataPropertyWrapper(ModelMetadata modelMetadata, IModelMetadataWrapperProvider modelMetadataWrapperProvider)
        {
            this.ModelMetadata = modelMetadata;
            this.ModelTypeId = ModelType.GetTypeId();
            modelMetadataWrapperProvider.RegisterModelType(this.ModelType);
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
