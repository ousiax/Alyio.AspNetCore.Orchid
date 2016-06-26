using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Extensions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.ViewModels
{
    /// <summary>
    /// Represents a group of related apis.
    /// </summary>
    public class ApiXDescriptionGroup
    {
        [JsonIgnore]
        public ApiDescriptionGroup ApiDescriptionGroup { get; }

        public ApiXDescriptionGroup(ApiDescriptionGroup apiDescriptionGroup)
        {
            this.ApiDescriptionGroup = apiDescriptionGroup;
        }

        /// <summary>
        /// The group name.
        /// </summary>
        public string GroupName { get { return this.ApiDescriptionGroup.GroupName; } }

        public IReadOnlyList<ApiXDescription> Items
        {
            get
            {
                return this.ApiDescriptionGroup
                    .Items
                    .Select(o => new ApiXDescription(o) { Id = o.GetFriendlyId() })
                    .ToList()
                    .AsReadOnly();
            }
        }
    }
}
