using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    /// <summary>
    /// A cached collection of <see cref="ApiDescriptionGroupWrapper" />.
    /// </summary>
    [DebuggerDisplay("Count = {Items.Count}")]
    internal class ApiDescriptionGroupCollectionWrapper : IApiDescriptionGroupCollectionWrapper
    {
        [JsonIgnore]
        public IServiceProvider ServiceProvider { get; set; }

        [JsonIgnore]
        public ApiDescriptionGroupCollection ApiDescriptionGroupCollection { get; }

        public ApiDescriptionGroupCollectionWrapper(ApiDescriptionGroupCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            this.ApiDescriptionGroupCollection = collection;
        }

        public IReadOnlyList<IApiDescriptionGroupWrapper> Items
        {
            get
            {
                return this.ApiDescriptionGroupCollection
                    .Items
                    .Select(o => new ApiDescriptionGroupWrapper(o) { ServiceProvider = this.ServiceProvider })
                    .ToList()
                    .AsReadOnly();
            }
        }

        public int Version => this.ApiDescriptionGroupCollection.Version;
    }
}
