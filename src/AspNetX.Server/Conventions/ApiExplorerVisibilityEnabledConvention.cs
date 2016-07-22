using System.Linq;
using Microsoft.AspNet.Mvc.ApplicationModels;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace AspNetX.Conventions
{
    public class ApiExplorerVisibilityEnabledConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (controller.ApiExplorer.IsVisible == null)
                {
                    if (controller.Attributes.OfType<IRouteTemplateProvider>().FirstOrDefault() != null)
                    {
                        controller.ApiExplorer.IsVisible = true;
                        controller.ApiExplorer.GroupName = controller.ControllerName;
                    }
                }
                else
                {
                    if (controller.ApiExplorer.GroupName == null)
                    {
                        controller.ApiExplorer.GroupName = controller.ControllerName;
                    }
                }
            }
        }
    }
}