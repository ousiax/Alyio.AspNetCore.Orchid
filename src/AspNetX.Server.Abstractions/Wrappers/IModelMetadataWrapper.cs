using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions
{
    public interface IModelMetadataWrapper
    {
        int Id { get; }

        Type ContainerType { get; }

        IModelMetadataWrapper ElementMetadataWrapper { get; }

        IReadOnlyDictionary<string, string> EnumNamesAndValues { get; }

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

        ModelMetadata ElementMetadata { get; }
    }
}