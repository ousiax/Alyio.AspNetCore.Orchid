using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions
{
    public interface IModelPropertyWrapper
    {
        int Id { get; }

        Type ContainerType { get; }

        ModelMetadataKind MetadataKind { get; }

        Type ModelType { get; }

        string PropertyName { get; }

        IModelMetadataWrapper MetadataWrapper { get; }
    }
}
