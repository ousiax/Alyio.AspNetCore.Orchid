using System;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions
{
    public interface IModelMetadataWrapper
    {
        Type ContainerType { get; }

        bool IsCollectionType { get; }

        bool IsComplexType { get; }

        bool IsEnum { get; }

        bool IsEnumerableType { get; }

        bool IsFlagsEnum { get; }

        bool IsNullableValueType { get; }

        bool IsReadOnly { get; }

        bool IsReferenceOrNullableType { get; }

        bool IsRequired { get; }

        ModelMetadataKind MetadataKind { get; }

        Type ModelType { get; }

        string PropertyName { get; }

        ModelMetadata Metadata { get; }
    }
}