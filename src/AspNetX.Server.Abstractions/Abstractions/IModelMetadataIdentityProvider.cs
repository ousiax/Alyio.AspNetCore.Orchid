using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Abstractions.Abstractions
{
    public interface IModelMetadataIdentityProvider
    {
        int GetId(ModelMetadataIdentity identity);

        ModelMetadataIdentity GetIdentity(int id);
    }
}
