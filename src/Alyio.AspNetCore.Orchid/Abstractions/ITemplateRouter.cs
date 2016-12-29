using Microsoft.AspNetCore.Routing;

namespace Alyio.AspNetCore.Orchid.Abstractions
{
    public interface ITemplateRouter : IRouter
    {
        string Template { get; }
    }
}
