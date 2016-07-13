using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AspNetX.Server.Abstractions;

namespace AspNetX.Server.Impl
{
    public class ApiDescriptionWrapperProvider : IApiDescriptionWrapperProvider
    {
        private readonly IReadOnlyDictionary<string, IApiDescriptionWrapper> _cache;

        public ApiDescriptionWrapperProvider(IApiDescriptionGroupCollectionWrapperProvider descriptionWrapperProvider)
        {
            var dict = descriptionWrapperProvider
                .ApiDescriptionGroupsWrapper
                .Items
                .SelectMany(o => o.Items)
                .Distinct()
                .ToDictionary(p => p.Id, p => p);
            this._cache = new ReadOnlyDictionary<string, IApiDescriptionWrapper>(dict);
        }

        public IApiDescriptionWrapper this[string key] => this._cache[key];

        public int Count => this._cache.Count;

        public IEnumerable<string> Keys => this._cache.Keys;

        public IEnumerable<IApiDescriptionWrapper> Values => this._cache.Values;

        public bool ContainsKey(string key) => this._cache.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, IApiDescriptionWrapper>> GetEnumerator() => this._cache.GetEnumerator();

        public bool TryGetValue(string key, out IApiDescriptionWrapper value) => this._cache.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this._cache.GetEnumerator();
    }
}
