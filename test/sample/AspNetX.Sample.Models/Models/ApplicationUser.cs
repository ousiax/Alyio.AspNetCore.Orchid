using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetX.Sample.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser Parent { get; set; }

        public IList<ApplicationUser> Children { get; set; }
    }
}
