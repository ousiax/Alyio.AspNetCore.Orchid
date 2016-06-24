using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ApiExplorer;

namespace AspNetX.Server.Controllers
{
    [Route("aspnetx/api/helppage")]
    public class HelpController : Controller
    {
        private readonly IApiDescriptionGroupCollectionProvider _descriptionProvider;

        public HelpController(IApiDescriptionGroupCollectionProvider descriptionProvider)
        {
            this._descriptionProvider = descriptionProvider;
        }

        [HttpGet()]
        public object Index()
        {
            return _descriptionProvider.ApiDescriptionGroups;
        }
    }
}
