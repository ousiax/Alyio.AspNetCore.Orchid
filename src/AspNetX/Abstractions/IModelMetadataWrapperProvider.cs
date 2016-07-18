using System;
using AspNetX.Models;

namespace AspNetX.Abstractions
{
    /// <summary>
    /// Providers to add or get <see cref="ModelMetadataWrapper"/>.
    /// </summary>
    public interface IModelMetadataWrapperProvider
    {
        /// <summary>
        /// Try to add a <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.ModelType"/>.
        /// </summary>
        /// <param name="modelType">The <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata.ModelType"/>.</param>
        /// <param name="modelMetadataWrapper">The <see cref="ModelMetadataWrapper"/>.</param>
        /// <returns>True if add success, otherwise false if <paramref name="modelType"/> existed.</returns>
        /// <remarks><paramref name="modelMetadataWrapper"/> will always be a instance of <see cref="ModelMetadataWrapper"/>.</remarks>
        bool TryAdd(Type modelType, out ModelMetadataWrapper modelMetadataWrapper);

        /// <summary>
        /// Try to get the <see cref="ModelMetadataWrapper"/> with a specified model type id.
        /// </summary>
        /// <param name="modelTypeId"><see cref="TypeExtensions.GetTypeId(Type)"/>.</param>
        /// <param name="modelMetadataWrapper">The <see cref="ModelMetadataWrapper"/>.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool TryGet(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper);
    }
}
