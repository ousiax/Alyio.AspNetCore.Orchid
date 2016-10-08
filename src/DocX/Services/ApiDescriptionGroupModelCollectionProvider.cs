using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DocX.Abstractions;
using DocX.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DocX.Services
{
    /// <inheritdoc />
    public class ApiDescriptionGroupModelCollectionProvider : IApiDescriptionGroupModelCollectionProvider
    {
        private readonly ILogger _logger;
        private readonly ServerOptions _serverOptions;
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        private readonly IDocumentationProvider _documentationProvider;
        private readonly Regex _obsoleteRouteRegex;

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
            IDocumentationProvider documentationProvider,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ApiDescriptionGroupModelCollectionProvider>();
            _serverOptions = serverOptions.Value;
            if (_serverOptions.ObsoleteRoutePathPattern != null)
            {
                try
                {
                    _obsoleteRouteRegex = new Regex(_serverOptions.ObsoleteRoutePathPattern, RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Invalid obsolete route pattern: {ex.Message}");
#if DEBUG
                    throw;
#endif
                }
            }
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _documentationProvider = documentationProvider;
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
            Type controllerType;
            if (TryGetControllerType(apiDescriptionGroup, out controllerType))
            {
                apiDescriptionGroupModel.Description = _documentationProvider.GetDocumentation(controllerType.GetTypeInfo());
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
            var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                apiDescriptionModel.IsDeprecated =
                    controllerActionDescriptor
                    .MethodInfo
                    .CustomAttributes
                    .Any(o => o.AttributeType == typeof(ObsoleteAttribute));
            }
            if (!apiDescriptionModel.IsDeprecated && _obsoleteRouteRegex != null) // filter obsolete api route with ObsoleteRoutePathPattern.
            {
                apiDescriptionModel.IsDeprecated = _obsoleteRouteRegex.IsMatch(apiDescriptionModel.RelativePath);
            }
            if (controllerActionDescriptor != null)
            {
                apiDescriptionModel.Description = _documentationProvider.GetDocumentation(controllerActionDescriptor.MethodInfo);
            }
            return apiDescriptionModel;
        }
    }
}
