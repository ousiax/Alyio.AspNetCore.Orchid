using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Alyio.AspNetCore.Orchid.Conventions
{
    public class ApiExplorerVisibilityEnabledConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
        }
    }
}