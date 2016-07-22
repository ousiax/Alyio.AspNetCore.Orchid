using Microsoft.AspNet.Routing;

namespace AspNetX.Abstractions
{
    public interface ITemplateRouter : IRouter
    {
        string Template { get; }
    }
}
