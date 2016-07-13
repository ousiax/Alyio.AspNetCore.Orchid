using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions
{
    public interface IModelMetadataIdentityProvider
    {
        int GetId(ModelMetadataIdentity identity);

        bool TryGetIdentity(int id, out ModelMetadataIdentity identity);
    }
}
