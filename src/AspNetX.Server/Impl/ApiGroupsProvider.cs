using AspNetX.Server.Abstractions;
using AspNetX.Server.Models;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server
{
    public class ApiGroupsProvider : IApiGroupsProvider
    {
        private readonly IApiDescriptionGroupCollectionProvider _descriptionProvider;

        public ApiGroupsProvider(IApiDescriptionGroupCollectionProvider descriptionProvider)
        {
            this._descriptionProvider = descriptionProvider;
        }

        public ApiXDescriptionGroupCollection ApiXDescriptionGroups
        {
            get { return new ApiXDescriptionGroupCollection(_descriptionProvider.ApiDescriptionGroups); }
        }
    }
}
