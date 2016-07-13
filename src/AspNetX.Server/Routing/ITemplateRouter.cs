using Microsoft.AspNetCore.Routing;

namespace AspNetX.Server.Routing
{
    public interface ITemplateRouter: IRouter
    {
        string RouteTemplate { get; }
    }
}
