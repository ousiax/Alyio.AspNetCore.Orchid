using System;
using System.Diagnostics;
using AspNetX.Server.Abstractions;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspNetX.Server.Wrappers
{
    [DebuggerDisplay("GroupName= {GroupName}, HttpMethod = {HttpMethod}, RelativePath = {RelativePath}")]
    internal class ApiDescriptionWrapper : IApiDescriptionWrapper
    {
        private IModelMetadataWrapper _responseModelMetadataWrapper;

        [JsonIgnore]
        public IServiceProvider ServiceProvider { get; set; }

        [JsonIgnore]
        public ApiDescription ApiDescription { get; }

        public string Id { get; }

        public string GroupName { get { return this.ApiDescription.GroupName; } set { this.ApiDescription.GroupName = value; } }

        public string HttpMethod { get { return this.ApiDescription.HttpMethod; } set { this.ApiDescription.HttpMethod = value; } }

        public string RelativePath { get { return this.ApiDescription.RelativePath; } set { this.ApiDescription.RelativePath = value; } }

        public IModelMetadataWrapper ResponseModelMetadataWrapper
        {
            get
            {
                string name = ApiDescription.ResponseModelMetadata?.ModelType.Name;

                if (_responseModelMetadataWrapper == null && ApiDescription.ResponseModelMetadata != null)
                {
                    _responseModelMetadataWrapper = ServiceProvider.GetService<IModelMetadataWrapperProvider>().GetModelMetadataWrapper(ApiDescription.ResponseModelMetadata);
                }
                return _responseModelMetadataWrapper;
            }
        }

        public ApiDescriptionWrapper(ApiDescription apiDescription)
        {
            this.ApiDescription = apiDescription;
            this.Id = apiDescription.GetFriendlyId();
        }

        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            string other = obj as string;
            return string.Equals(this.Id, other, StringComparison.OrdinalIgnoreCase);
        }
    }
}