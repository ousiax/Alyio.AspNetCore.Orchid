using System.Collections.Generic;
using System.Linq;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Models
{
    public class ApiDescriptionGroupWrapper: IApiDescriptionGroupWrapper
    {
        [JsonIgnore]
        public ApiDescriptionGroup ApiDescriptionGroup { get; }

        public ApiDescriptionGroupWrapper(ApiDescriptionGroup apiDescriptionGroup)
        {
            this.ApiDescriptionGroup = apiDescriptionGroup;
        }

        public string GroupName { get { return this.ApiDescriptionGroup.GroupName; } }

        public IReadOnlyList<IApiDescriptionWrapper> Items
        {
            get
            {
                return this.ApiDescriptionGroup
                    .Items
                    .Select(o => new ApiDescriptionWrapper(o))
                    .ToList()
                    .AsReadOnly();
            }
        }
    }
}
