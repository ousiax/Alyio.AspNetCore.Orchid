using System.Reflection;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApplicationModels;

namespace AspNetX.Server
{
    public class ApiExplorerVisibilityDisabledConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
#if NET451
                if (controller.ControllerType.GetTypeInfo().GetCustomAttribute<RouteAttribute>() == null)
#else
                if (controller.ControllerType.GetCustomAttribute<RouteAttribute>() == null)
#endif
                {
                    controller.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}