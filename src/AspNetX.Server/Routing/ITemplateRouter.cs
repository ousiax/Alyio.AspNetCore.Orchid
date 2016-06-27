using Microsoft.AspNet.Routing;

namespace AspNetX.Server.Routing
{
    public interface ITemplateRouter: IRouter
    {
        string RouteTemplate { get; }
    }
}
