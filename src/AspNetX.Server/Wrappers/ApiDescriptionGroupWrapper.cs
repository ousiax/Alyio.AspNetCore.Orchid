using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    [DebuggerDisplay("Count = {Items.Count}")]
    internal class ApiDescriptionGroupWrapper : IApiDescriptionGroupWrapper
    {
        [JsonIgnore]
        public IServiceProvider ServiceProvider { get; set; }

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
                    .Select(o => new ApiDescriptionWrapper(o) { ServiceProvider = this.ServiceProvider })
                    .ToList()
                    .AsReadOnly();
            }
        }
    }
}
