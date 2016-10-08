using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DocX.Models
{
    /// <summary>
    /// A cached collection of <see cref="ApiDescriptionGroupModel" />.
    /// </summary>
    [DataContract]
    public class ApiDescriptionGroupModelCollection
    {
        /// <summary>
        /// Gets or sets a general description of discovered groups.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Returns the list of <see cref="IReadOnlyList{ApiDescriptionGroup}"/>.
        /// </summary>
        [DataMember(Name = "items")]
        public IReadOnlyList<ApiDescriptionGroupModel> Items { get; private set; }

        /// <summary>
        /// Returns the unique version of the current items.
        /// </summary>
        [DataMember(Name = "version")]
        public int Version { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDescriptionGroupModelCollection"/>.
        /// </summary>
        /// <param name="items">The list of <see cref="ApiDescriptionGroupModel"/>.</param>
        /// <param name="version">The unique version of discovered groups.</param>
        public ApiDescriptionGroupModelCollection(IReadOnlyList<ApiDescriptionGroupModel> items, int version)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Items = items;
            Version = version;
        }
    }
}
