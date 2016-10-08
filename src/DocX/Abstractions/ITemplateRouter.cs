using Microsoft.AspNetCore.Routing;

namespace DocX.Abstractions
{
    public interface ITemplateRouter : IRouter
    {
        string Template { get; }
    }
}
