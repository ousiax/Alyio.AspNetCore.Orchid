using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetX.Server.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApiExplorer;
using Newtonsoft.Json;

namespace AspNetX.Server.Controllers
{
    [Route("aspnetx")]
    public class ApiDescriptionController : Controller
    {
        private readonly IApiDescriptionGroupCollectionProvider _descriptionProvider;

        public ApiDescriptionController(IApiDescriptionGroupCollectionProvider descriptionProvider)
        {
            this._descriptionProvider = descriptionProvider;
        }

        [HttpGet("apigroups")]
        public IActionResult Index()
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            return Json(new ApiXDescriptionGroupCollection(_descriptionProvider.ApiDescriptionGroups), settings);
        }
    }
}
