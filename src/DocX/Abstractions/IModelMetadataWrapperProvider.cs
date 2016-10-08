using System;
using DocX.Models;

namespace DocX.Abstractions
{
    /// <summary>
    /// Providers to add or get <see cref="ModelMetadataWrapper"/>.
    /// </summary>
    public interface IModelMetadataWrapperProvider
    {
        void RegisterModelType(Type modelType);

        void RegisterModelMetadataWrapper(ModelMetadataWrapper modelMetadataWrapper);

        ModelMetadataWrapper GetOrCreate(Type modelType);

        /// <summary>
        /// Try to get the <see cref="ModelMetadataWrapper"/> with a specified model type id.
        /// </summary>
        /// <param name="modelTypeId"><see cref="TypeExtensions.GetTypeId(Type)"/>.</param>
        /// <param name="modelMetadataWrapper">The <see cref="ModelMetadataWrapper"/>.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool TryGet(string modelTypeId, out ModelMetadataWrapper modelMetadataWrapper);
    }
}
