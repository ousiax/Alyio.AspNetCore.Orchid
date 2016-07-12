using System;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Wrappers;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Impl
{
    public class ApiDescriptionGroupCollectionWrapperProvider : IApiDescriptionGroupCollectionWrapperProvider
    {
        private readonly IApiDescriptionGroupCollectionWrapper _apiDescriptionGroupsWrapper;

        public ApiDescriptionGroupCollectionWrapperProvider(IServiceProvider resolver)
        {
            var descriptionProvider = resolver.GetService<IApiDescriptionGroupCollectionProvider>();
            _apiDescriptionGroupsWrapper = new ApiDescriptionGroupCollectionWrapper(descriptionProvider.ApiDescriptionGroups) { ServiceProvider = resolver };
        }

        public IApiDescriptionGroupCollectionWrapper ApiDescriptionGroupsWrapper
        {
            get { return _apiDescriptionGroupsWrapper; }
        }
    }
}
