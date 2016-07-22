using System;
using System.Linq;
using AspNetX.Abstractions;
using AspNetX.Models;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.Extensions.OptionsModel;

namespace AspNetX.Services
{
    /// <inheritdoc />
    public class ApiDescriptionGroupModelCollectionProvider : IApiDescriptionGroupModelCollectionProvider
    {
        private readonly ServerOptions _serverOptions;
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        private readonly IDocumentationProvider _documentationProvider;

        private ApiDescriptionGroupModelCollection _apiDescriptionGroups;

        /// <summary>
        /// Creates a new instance of <see cref="ApiDescriptionGroupCollectionProvider"/>.
        /// </summary>
        /// <param name="apiDescriptionGroupCollectionProvider">
        /// The <see cref="IApiDescriptionGroupCollectionProvider"/>.
        /// </param>
        public ApiDescriptionGroupModelCollectionProvider(
            IOptions<ServerOptions> serverOptions,
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            IDocumentationProviderFactory documentationProviderFactory)
        {
            _serverOptions = serverOptions.Value;
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _documentationProvider = documentationProviderFactory.Create();
        }

        /// <inheritdoc />
        public ApiDescriptionGroupModelCollection ApiDescriptionGroups
        {
            get
            {
                var apiGroups = _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups;
                if (_apiDescriptionGroups == null || _apiDescriptionGroups.Version != apiGroups.Version)
                {
                    _apiDescriptionGroups = GetCollection(apiGroups);
                }
                return _apiDescriptionGroups;
            }
        }

        private ApiDescriptionGroupModelCollection GetCollection(ApiDescriptionGroupCollection apiGroups)
        {
            var items = apiGroups
                .Items
                .Select(CreateApiDescriptionGroupModel)
                .ToList()
                .AsReadOnly();

            return new ApiDescriptionGroupModelCollection(items, apiGroups.Version) { Description = _serverOptions.Description };
        }

        private ApiDescriptionGroupModel CreateApiDescriptionGroupModel(ApiDescriptionGroup apiDescriptionGroup)
        {
            var apiDescriptionGroupModel = new ApiDescriptionGroupModel(
                             apiDescriptionGroup.GroupName,
                             apiDescriptionGroup.Items
                             .Select(CreateApiDescriptionModel)
                             .ToList()
                             .AsReadOnly());
            if (_documentationProvider != null)
            {
                Type controllerType;
                if (TryGetControllerType(apiDescriptionGroup, out controllerType))
                {
                    apiDescriptionGroupModel.Description = _documentationProvider.GetDocumentation(controllerType);
                }
            }
            return apiDescriptionGroupModel;
        }

        private static bool TryGetControllerType(ApiDescriptionGroup apiDescriptionGroup, out Type controllerType)
        {
            controllerType = null;

            var controllerActionDescriptor = apiDescriptionGroup.Items.Select(o => o.ActionDescriptor).OfType<ControllerActionDescriptor>().FirstOrDefault();
            if (controllerActionDescriptor != null)
            {
                controllerType = controllerActionDescriptor.ControllerTypeInfo.AsType();
            }
            return controllerType != null;
        }

        private ApiDescriptionModel CreateApiDescriptionModel(ApiDescription apiDescription)
        {
            var apiDescriptionModel = new ApiDescriptionModel
            {
                Description = null,
                Id = apiDescription.GetFriendlyId(),
                GroupName = apiDescription.GroupName,
                HttpMethod = apiDescription.HttpMethod,
                RelativePath = apiDescription.RelativePath,
                ApiDescription = apiDescription
            };
            if (_documentationProvider != null)
            {
                var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
                if (controllerActionDescriptor != null)
                {
                    apiDescriptionModel.Description = _documentationProvider.GetDocumentation(controllerActionDescriptor.MethodInfo);
                }
            }
            return apiDescriptionModel;
        }
    }
}
