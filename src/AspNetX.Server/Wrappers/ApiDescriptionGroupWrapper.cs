using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    [DebuggerDisplay("Count = {Items.Count}")]
    internal class ApiDescriptionGroupWrapper : IApiDescriptionGroupWrapper
    {
        [JsonIgnore]
        public ApiDescriptionGroup ApiDescriptionGroup { get; }

        public string GroupName { get { return this.ApiDescriptionGroup.GroupName; } }

        public IReadOnlyList<IApiDescriptionWrapper> Items { get; }

        public ApiDescriptionGroupWrapper(ApiDescriptionGroup apiDescriptionGroup, IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            if (apiDescriptionGroup == null)
            {
                throw new ArgumentNullException(nameof(apiDescriptionGroup));
            }
            if (metadataWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapperProvider));
            }

            this.ApiDescriptionGroup = apiDescriptionGroup;

            this.Items = this.ApiDescriptionGroup
                    .Items
                    .Select(o => new ApiDescriptionWrapper(o, metadataWrapperProvider))
                    .ToList()
                    .AsReadOnly();
        }
    }
}
