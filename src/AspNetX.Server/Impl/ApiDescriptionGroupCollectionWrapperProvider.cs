using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Impl
{
    public class ApiDescriptionGroupCollectionWrapperProvider : IApiDescriptionGroupCollectionWrapperProvider
    {
        public ApiDescriptionGroupCollectionWrapperProvider(IApiDescriptionGroupCollectionProvider descriptionProvider, IModelMetadataWrapperProvider metadataWrapperProvider)
        {
            this.ApiDescriptionGroupsWrapper = new ApiDescriptionGroupCollectionWrapper(descriptionProvider.ApiDescriptionGroups, metadataWrapperProvider);
        }

        public IApiDescriptionGroupCollectionWrapper ApiDescriptionGroupsWrapper { get; }
    }
}
