using System;
using System.Diagnostics;
using AspNetX.Server.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System.Linq;

namespace AspNetX.Server.Wrappers
{
    [DebuggerDisplay("Id = {Id}")]
    internal class ApiDescriptionWrapper : IApiDescriptionWrapper
    {
        [JsonIgnore]
        public ApiDescription ApiDescription { get; }

        public string Id { get; }

        public string GroupName { get { return this.ApiDescription.GroupName; } set { this.ApiDescription.GroupName = value; } }

        public string HttpMethod { get { return this.ApiDescription.HttpMethod; } set { this.ApiDescription.HttpMethod = value; } }

        public string RelativePath { get { return this.ApiDescription.RelativePath; } set { this.ApiDescription.RelativePath = value; } }

        public IModelMetadataWrapper ResponseModelMetadataWrapper { get; }

        public ApiDescriptionWrapper(ApiDescription apiDescription, IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            if (apiDescription == null)
            {
                throw new ArgumentNullException(nameof(apiDescription));
            }
            if (metadataWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(metadataWrapperProvider));
            }

            this.ApiDescription = apiDescription;
            this.Id = apiDescription.GetFriendlyId();

            var responseModelMetadata = ApiDescription.SupportedResponseTypes.FirstOrDefault()?.ModelMetadata;
            if (responseModelMetadata != null)   //TODO ApiDescription.SupportedResponseTypes
            {
                ResponseModelMetadataWrapper = metadataWrapperProvider.GetModelMetadataWrapper(responseModelMetadata);
            }
        }

        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            IApiDescriptionWrapper other = obj as IApiDescriptionWrapper;
            if (other != null)
            {
                return string.Equals(this.Id, other.Id, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}