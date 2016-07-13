using System;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Converters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetX.Server.Wrappers
{
    public class ModelPropertyWrapper : IModelPropertyWrapper
    {
        public int Id => MetadataWrapper.Id;

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ContainerType => MetadataWrapper.ContainerType;

        [JsonConverter(typeof(StringEnumConverter))]
        public ModelMetadataKind MetadataKind => MetadataWrapper.MetadataKind;

        [JsonConverter(typeof(ModelTypeConverter))]
        public Type ModelType => MetadataWrapper.ModelType;

        public string PropertyName => MetadataWrapper.PropertyName;

        [JsonIgnore]
        public IModelMetadataWrapper MetadataWrapper { get; }

        public ModelPropertyWrapper(IModelMetadataWrapper metadataWrapper)
        {
            if (metadataWrapper == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapper));
            }

            this.MetadataWrapper = metadataWrapper;

            if (this.MetadataKind != ModelMetadataKind.Property)
            {
                throw new ArgumentOutOfRangeException(nameof(metadataWrapper), "metadataWrapper's MetadataKind MUST be ModelMetadataKind.Property.");
            }
        }
    }
}
