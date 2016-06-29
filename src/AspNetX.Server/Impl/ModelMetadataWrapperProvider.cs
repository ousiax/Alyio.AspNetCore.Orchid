using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Impl
{
    public class ModelMetadataWrapperProvider : IModelMetadataWrapperProvider
    {
        private readonly IModelMetadataIdentityProvider _identityProvider;
        private readonly IDictionary<int, IModelMetadataWrapper> _metadataWrapperCache;

        public ModelMetadataWrapperProvider(IModelMetadataIdentityProvider identityProvider)
        {
            if (identityProvider == null)
            {
                throw new ArgumentNullException(nameof(identityProvider));
            }
            _identityProvider = identityProvider;
            _metadataWrapperCache = new ConcurrentDictionary<int, IModelMetadataWrapper>();
        }

        public bool TryGetModelMetadataWrapper(int id, out IModelMetadataWrapper wrapper)
        {
            return _metadataWrapperCache.TryGetValue(id, out wrapper);
        }

        public IModelMetadataWrapper GetModelMetadataWrapper(ModelMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            ModelMetadataIdentity identity = metadata.ToMetadataIdentity();
            var id = _identityProvider.GetId(identity);

            IModelMetadataWrapper metadataWrapper;
            if (!TryGetModelMetadataWrapper(id, out metadataWrapper))
            {
                metadataWrapper = new ModelMetadataWrapper(metadata, _identityProvider, this);
                _metadataWrapperCache.Add(id, metadataWrapper);
            }
            return metadataWrapper;
        }

        public bool TryGetModelPropertyWrapper(int id, out IModelPropertyWrapper wrapper)
        {
            IModelMetadataWrapper metadataWrapper;
            if (TryGetModelMetadataWrapper(id, out metadataWrapper))
            {
                wrapper = new ModelPropertyWrapper(metadataWrapper);
                return true;
            }
            wrapper = null;
            return false;
        }

        public IModelPropertyWrapper GetModelPropertyWrapper(ModelMetadata metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException(nameof(metadata));
            }
            IModelMetadataWrapper metadataWrapper = GetModelMetadataWrapper(metadata);
            return new ModelPropertyWrapper(metadataWrapper);
        }
    }
}
