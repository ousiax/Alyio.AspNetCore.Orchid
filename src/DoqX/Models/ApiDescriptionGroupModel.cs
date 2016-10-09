using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DoqX.Models
{
    /// <summary>
    /// Represents a group of related apis.
    /// </summary>
    [DataContract]
    public class ApiDescriptionGroupModel
    {
        /// <summary>
        /// Creates a new <see cref="ApiDescriptionGroupModel"/>.
        /// </summary>
        /// <param name="groupName">The group name.</param>
        /// <param name="items">A collection of <see cref="ApiDescriptionModel"/> items for this group.</param>
        public ApiDescriptionGroupModel(string groupName, IReadOnlyList<ApiDescriptionModel> items)
        {
            GroupName = groupName;
            Items = items;
        }

        /// <summary>
        /// Gets or sets a general description of this group.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The group name.
        /// </summary>
        [DataMember(Name = "groupName")]
        public string GroupName { get; private set; }

        /// <summary>
        /// A collection of <see cref="ApiDescriptionModel"/> items for this group.
        /// </summary>
        [DataMember(Name = "items")]
        public IReadOnlyList<ApiDescriptionModel> Items { get; private set; }

        public ApiDescriptionGroup ApiDescriptionGroup { get; set; }
    }
}
