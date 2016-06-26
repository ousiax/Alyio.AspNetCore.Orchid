using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Models
{ /// <summary>
  /// A cached collection of <see cref="ApiDescriptionGroup" />.
  /// </summary>
    public class ApiXDescriptionGroupCollection
    {
        [JsonIgnore]
        public ApiDescriptionGroupCollection ApiDescriptionGroupCollection { get; }

        public ApiXDescriptionGroupCollection(ApiDescriptionGroupCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            this.ApiDescriptionGroupCollection = collection;
        }

        /// <summary>
        /// Returns the list of <see cref="IReadOnlyList{ApiXDescriptionGroup}"/>.
        /// </summary>
        public IReadOnlyList<ApiXDescriptionGroup> Items { get { return this.ApiDescriptionGroupCollection.Items.Select(o => new ApiXDescriptionGroup(o)).ToList().AsReadOnly(); } }

        /// <summary>
        /// Returns the unique version of the current items.
        /// </summary>
        public int Version { get { return this.ApiDescriptionGroupCollection.Version; } }
    }
}
