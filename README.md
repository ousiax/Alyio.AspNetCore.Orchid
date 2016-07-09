The AspNetX automatically generates help page content for the web APIs on your site.

AspNetX can be added to your in app in just a few simple steps:

- Add a dependency on the "AspNetX.Server" package in `project.json`
- Call `services.AddAspNetX()` in `ConfigureServices`
- Call `app.UseAspNetX` in `Configure`

Now, and when the user browses to *http://&lt;yourApp&gt;/aspnetx*, and you should see Web Api information in the browser window. 