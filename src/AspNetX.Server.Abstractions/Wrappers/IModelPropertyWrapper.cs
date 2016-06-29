using System;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions
{
    public interface IModelPropertyWrapper
    {
        int Id { get; }

        Type ContainerType { get; }

        ModelMetadataKind MetadataKind { get; }

        Type ModelType { get; }

        string PropertyName { get; }

        ModelMetadata Metadata { get; }
    }
}
