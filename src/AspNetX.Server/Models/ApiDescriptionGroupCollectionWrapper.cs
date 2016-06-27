using System;
using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Models
{
    /// <summary>
    /// A cached collection of <see cref="ApiDescriptionGroupWrapper" />.
    /// </summary>
    public class ApiDescriptionGroupCollectionWrapper : IApiDescriptionGroupCollectionWrapper
    {
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

        public IReadOnlyList<IApiDescriptionGroupWrapper> Items { get { return this.ApiDescriptionGroupCollection.Items.Select(o => new ApiDescriptionGroupWrapper(o)).ToList().AsReadOnly(); } }

        public int Version => this.ApiDescriptionGroupCollection.Version;
    }
}
