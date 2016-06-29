using Microsoft.AspNet.Mvc.ModelBinding;

namespace AspNetX.Server.Abstractions
{
    public interface IModelMetadataWrapperProvider
    {
        bool TryGetModelMetadataWrapper(int id, out IModelMetadataWrapper wrapper);

        IModelMetadataWrapper GetModelMetadataWrapper(ModelMetadata metadata);

        bool TryGetModelPropertyWrapper(int id, out IModelPropertyWrapper wrapper);

        IModelPropertyWrapper GetModelPropertyWrapper(ModelMetadata metadata);
    }
}
