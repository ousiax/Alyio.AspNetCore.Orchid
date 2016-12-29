using Microsoft.AspNetCore.Routing;

namespace Orchid.Abstractions
{
    public interface ITemplateRouter : IRouter
    {
        string Template { get; }
    }
}
