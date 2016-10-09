using Microsoft.AspNetCore.Routing;

namespace DoqX.Abstractions
{
    public interface ITemplateRouter : IRouter
    {
        string Template { get; }
    }
}
