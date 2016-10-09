```
```
```
你不是讲过，被我气都气饱了？这气饱的，果然比不上吃东西填肚子，一会儿就撑不住了。 云天河 -《仙剑奇侠传 4》
```

```text
道贯三才为一气耳，

天以气而运行，

地以气而发生，

阴阳以气而惨舒，

风雷以气而动荡，

人身以气而呼吸，

道法以气而感通。

水之润下，无孔不入；

火之炎上，无物不焚；

雷之肃敛，无坚不摧；

风之肆拂，无阻不透；

土之养化，无物不融!
```

```cs
using DoqX;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SinaWeibo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServerOptions>(o => o.Description = "无所谓好或不好，人生一场虚空大梦，韶华白首，不过转瞬。惟有天道恒在，往复循环，不曾更改…… —— 慕容紫英.仙剑奇侠传 4》");
            services.AddDocX();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseDocX();
            app.UseMvc();
        }
    }
}
```