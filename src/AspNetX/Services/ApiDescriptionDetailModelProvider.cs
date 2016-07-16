using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AspNetX.Abstractions;
using AspNetX.Models;

namespace AspNetX.Services
{
    public class ApiDescriptionDetailModelProvider : IApiDescriptionDetailModelProvider
    {
        private readonly IReadOnlyDictionary<string, ApiDescriptionDetailModel> _apiDescriptionDetailModelCache;

        public ApiDescriptionDetailModelProvider(IApiDescriptionGroupModelCollectionProvider apiDescriptionGroupModelCollectionProvider)
        {
            var dictionary = apiDescriptionGroupModelCollectionProvider
                .ApiDescriptionGroups
                .Items
                .SelectMany(g => g.Items)
                .ToDictionary(o => o.Id, o => CreateApiDescriptionDetailModel(o));   //TODO Maybe exits dumpliate ids.
            _apiDescriptionDetailModelCache = new ReadOnlyDictionary<string, ApiDescriptionDetailModel>(dictionary);
        }

        public ApiDescriptionDetailModel this[string key] => _apiDescriptionDetailModelCache[key];

        public int Count => _apiDescriptionDetailModelCache.Count;

        public IEnumerable<string> Keys => _apiDescriptionDetailModelCache.Keys;

        public IEnumerable<ApiDescriptionDetailModel> Values => _apiDescriptionDetailModelCache.Values;

        public bool ContainsKey(string key) => _apiDescriptionDetailModelCache.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, ApiDescriptionDetailModel>> GetEnumerator() => _apiDescriptionDetailModelCache.GetEnumerator();

        public bool TryGetValue(string key, out ApiDescriptionDetailModel value) => _apiDescriptionDetailModelCache.TryGetValue(key, out value);

        private ApiDescriptionDetailModel CreateApiDescriptionDetailModel(ApiDescriptionModel apiDescriptionModel)
        {
            return new ApiDescriptionDetailModel
            {
                Id = apiDescriptionModel.Id,
                Description = apiDescriptionModel.Description,
                GroupName = apiDescriptionModel.GroupName,
                HttpMethod = apiDescriptionModel.HttpMethod,
                RelativePath = apiDescriptionModel.RelativePath
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => _apiDescriptionDetailModelCache.GetEnumerator();
    }
}
