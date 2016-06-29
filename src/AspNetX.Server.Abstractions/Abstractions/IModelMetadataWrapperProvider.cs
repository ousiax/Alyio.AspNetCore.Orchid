namespace AspNetX.Server.Abstractions
{
    public interface IModelMetadataWrapperProvider
    {
        bool TryGetModelMetadataWrapper(int id, out IModelMetadataWrapper wrapper);
    }
}
