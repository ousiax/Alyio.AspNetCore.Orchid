# DoqX

The DoqX automatically generates help page content for the web APIs on your site.

### Usage

DoqX can be added to your in app in just a few simple steps:

- Add a dependency on the "DoqX" package in `project.json`
- Call `services.AddDoqX` in `ConfigureServices`
- Call `app.UseDoqX` in `Configure`

And when you browse to *http://&lt;yourApp&gt;/api-docs*, you should see Web APIs information in the web browser. 

### Customization

You can also custom the DoqX with `services.Configure<ServerOptions>` about you app and write a *about.md* in the `IHostingEnvironment.ContentRoot` to generate the about page.

By default, *DoqX* just collects the API `Controller` annotated with `RouteAttribute` or `[ApiExplorerSettings(IgnoreApi = false)]`, and ignores others.

An example *Startup.cs*.

```cs
using DoqX;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ServerOptions>(o => o.Description = "无所谓好或不好，人生一场虚空大梦，韶华白首，不过转瞬。惟有天道恒在，往复循环，不曾更改…… —— 慕容紫英.仙剑奇侠传 4》");
        services.AddDoqX();
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDoqX();
        app.UseMvc();
    }
}
```

### Features

1. Auto generate Web API description.

    - Http method
    - Relative path
    - URL or query paramter
    - Body parameter
    - Request format and samples
    - Response format and samples
    - Model or entity metadata

2. Highlight http verbs.

    For example, the `DELETE` method, we use the red color, and `GET` with blue color.

3. Auto load xml documentation description.

    Set the `xmlDoc` as `true` to generate XML documentation from triple-slash comments in the source code.

    ```
    {
        "buildOptions": {
            "xmlDoc": true
        }
    }
    ```

### Support frameworks

- .NET framework 4.5.1
- .NET standard 1.6

### Support platforms

- ASP.NET Core 1.0
- ASP.NET RC1

### Other

You can get the latest release from the official [Nuget.org](https://www.nuget.org/packages/DoqX/) feed or from its [github releases page](https://github.com/qqbuby/DoqX/releases).

For **ASP.NET DNX RC1** app, please get this version `1.0.2-rc1-final` from the offical [Nuget.org](https://www.nuget.org/packages/AspNetX.Server/1.0.2-rc1-final) or from its github [releases](https://github.com/qqbuby/DoqX/releases/tag/v1.0.2-rc1-final) page.
