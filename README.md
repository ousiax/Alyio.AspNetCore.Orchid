# AspNetX [Beta version on ASP.NET Core 1.0]

You can get the latest release from the official [Nuget.org](https://www.nuget.org/packages/AspNetX/) feed or from its [github releases page](https://github.com/qqbuby/AspNetX/releases).

The AspNetX automatically generates help page content for the web APIs on your site.

AspNetX can be added to your in app in just a few simple steps:

- Add a dependency on the "AspNetX" package in `project.json`
- Call `services.AddAspNetX()` in `ConfigureServices`
- Call `app.UseAspNetX()` in `Configure`

Now, and when the user browses to *http://&lt;yourApp&gt;/api-docs*, and you should see Web Api information in the web browser. 

By default, *AspNetX* just collects these API `Controller`s that are annotated with `RouteAttribute` or `[ApiExplorerSettings(IgnoreApi = false)]`, and ignores others.

You can also custom some options of AspNetX with `ServerOptions`.

An example *Startup.cs*.

```cs
using AspNetX;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ServerOptions>(o => o.Description = "无所谓好或不好，人生一场虚空大梦，韶华白首，不过转瞬。惟有天道恒在，往复循环，不曾更改…… —— 慕容紫英.仙剑奇侠传 4》");
        services.AddAspNetX();
        // Add framework services.
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseAspNetX();
        app.UseMvc();
    }
}
```

### Others

For **ASP.NET DNX RC1** app, please get this version `1.0.0-beta-65536` from the offical [Nuget.org](http://www.nuget.org/packages/AspNetX.Server/1.0.0-beta-65536) or from its github [releases](https://github.com/qqbuby/AspNetX/releases/tag/v1.0.0-beta-65536) page.
