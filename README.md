# AspNetX [Beta version]

You can get the latest release from the [official Nuget.org](https://www.nuget.org/packages/AspNetX.Server/) feed or from its [github releases page](https://github.com/qqbuby/AspNetX/releases).

The AspNetX automatically generates help page content for the web APIs on your site.

AspNetX can be added to your in app in just a few simple steps:

- Add a dependency on the "AspNetX.Server" package in `project.json`
- Call `services.AddAspNetX()` in `ConfigureServices`
- Call `app.UseAspNetX()` in `Configure`

Now, and when the user browses to *http://&lt;yourApp&gt;/aspnetx*, and you should see Web Api information in the web browser. 

### Notice!

By default, *AspNetX* just collects these API `Controller`s that are annotated with `RouteAttribute`, and ignores others.

e.g. *ApiController*

```cs
using Microsoft.AspNet.Mvc;

namespace AspNetX.Mvc.Sample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
```
