using System;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Converters;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Server.Wrappers
{
    public class ModelPropertyWrapper : IModelPropertyWrapper
    {
        public int Id { get; }

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ContainerType => Metadata.ContainerType;

        [JsonConverter(typeof(StringEnumConverter))]
        public ModelMetadataKind MetadataKind => Metadata.MetadataKind;

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ModelType => Metadata.ModelType;

        public string PropertyName => Metadata.PropertyName;

        [JsonIgnore]
        public ModelMetadata Metadata { get; }

        public ModelPropertyWrapper(ModelMetadata metadata, IModelMetadataIdentityProvider identityProvider)
        {
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));
            if (metadata.MetadataKind != ModelMetadataKind.Property) throw new ArgumentOutOfRangeException(nameof(metadata), "metadata's MetadataKind MUST be ModelMetadataKind.Property.");
            this.Metadata = metadata;
            this.Id = identityProvider.GetId(ModelMetadataIdentity.ForProperty(ModelType, PropertyName, ContainerType));
        }
    }
}
