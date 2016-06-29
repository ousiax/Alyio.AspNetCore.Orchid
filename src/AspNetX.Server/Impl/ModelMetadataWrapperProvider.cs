using System;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Impl
{
    public class ModelMetadataWrapperProvider : IModelMetadataWrapperProvider
    {
        private readonly IModelMetadataIdentityProvider _identityProvider;
        private readonly IModelMetadataProvider _metadataProvider;

        public ModelMetadataWrapperProvider(IModelMetadataIdentityProvider identityProvider, IModelMetadataProvider metadataProvider)
        {
            _identityProvider = identityProvider;
            _metadataProvider = metadataProvider;
        }

        public bool TryGetModelMetadataWrapper(int id, out IModelMetadataWrapper wrapper)
        {
            ModelMetadataIdentity identity;
            if (_identityProvider.TryGetIdentity(id, out identity))
            {
                var metadata = _metadataProvider.GetMetadataForType(identity.ModelType);
                wrapper = new ModelMetadataWrapper(metadata, _identityProvider);
                return true;
            }
            wrapper = null;
            return false;
        }

        public IModelMetadataWrapper GetModelMetadataWrapper(ModelMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            ModelMetadataIdentity identity;
            if (metadata.MetadataKind == ModelMetadataKind.Property)
            {
                identity = ModelMetadataIdentity.ForProperty(metadata.ModelType, metadata.PropertyName, metadata.ContainerType);
            }
            else
            {
                identity = ModelMetadataIdentity.ForType(metadata.ModelType);
            }
            var id = _identityProvider.GetId(identity);

            return new ModelMetadataWrapper(metadata, _identityProvider);
        }
    }
}
