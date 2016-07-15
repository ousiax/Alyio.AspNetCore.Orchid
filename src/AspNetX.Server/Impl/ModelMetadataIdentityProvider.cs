using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace AspNetX.Server.Impl
{
    public class ModelMetadataIdentityProvider : IModelMetadataIdentityProvider
    {
        private readonly ModelMetadataIdentityCache _identityCache;
        private readonly IdGenerator _idGenerator;

        public ModelMetadataIdentityProvider()
        {
            this._identityCache = new ModelMetadataIdentityCache();
            this._idGenerator = new IdGenerator();
        }

        public int GetId(ModelMetadataIdentity identity)
        {
            int id;
            if (!_identityCache.TryGetValue(identity, out id))
            {
                id = this._idGenerator.Next();
                _identityCache.Add(id, identity);
            }
            return id;
        }

        public bool TryGetIdentity(int id, out ModelMetadataIdentity identity)
        {
            return _identityCache.TryGetValue(id, out identity);
        }

        private class IdGenerator
        {
            private int _id;

            public IdGenerator()
            {
                this._id = 0;
            }

            public int Next()
            {
                return Interlocked.Increment(ref _id);
            }
        }

        private class ModelMetadataIdentityCache
        {
            private IDictionary<ModelMetadataIdentity, int> _metadataCache;
            private IDictionary<int, ModelMetadataIdentity> _idCache;

            public ModelMetadataIdentityCache()
            {
                this._metadataCache = new ConcurrentDictionary<ModelMetadataIdentity, int>();
                this._idCache = new ConcurrentDictionary<int, ModelMetadataIdentity>();
            }

            #region { ModelMetadataIdentity, Int32 }

            public void Add(ModelMetadataIdentity key, int value)
            {
                try
                {
                    this._metadataCache.Add(key, value);
                    this._idCache.Add(value, key);
                }
                catch
                {
                    this.Remove(key);
                    throw;
                }
            }

            public bool ContainsKey(ModelMetadataIdentity key)
            {
                return this._metadataCache.ContainsKey(key);
            }

            public bool Remove(ModelMetadataIdentity key)
            {
                int id;
                if (TryGetValue(key, out id))
                {
                    return this._metadataCache.Remove(key) && this._idCache.Remove(id);
                }
                return false;
            }

            public bool TryGetValue(ModelMetadataIdentity key, out int value)
            {
                return this._metadataCache.TryGetValue(key, out value);
            }

            #endregion

            #region { Int32, ModelMetadataIdentity }

            public void Add(int key, ModelMetadataIdentity value)
            {
                try
                {
                    this._metadataCache.Add(value, key);
                    this._idCache.Add(key, value);
                }
                catch
                {
                    this.Remove(key);
                    throw;
                }
            }

            public bool ContainsKey(int key)
            {
                return this._idCache.ContainsKey(key);
            }

            public bool Remove(int key)
            {
                ModelMetadataIdentity identity;
                if (TryGetValue(key, out identity))
                {
                    return this._metadataCache.Remove(identity) && this._idCache.Remove(key);
                }
                return false;
            }

            public bool TryGetValue(int key, out ModelMetadataIdentity value)
            {
                return this._idCache.TryGetValue(key, out value);
            }

            #endregion
        }
    }
}
