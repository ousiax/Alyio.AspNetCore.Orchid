using System;
using System.Collections.Generic;
using AspNetX.Sample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetX.Sample.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationUserController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/values
        [HttpPost]
        public ApplicationUser Post([FromBody]ApplicationUser value)
        {
            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ApplicationUser value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
