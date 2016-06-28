using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspNetX.Mvc.Sample.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Hello World
        /// </summary>
        [NotMapped]
        public IList<string> MyProperty { get; set; }

        [NotMapped]
        public BindingFlags MyProperty2 { get; set; }

        [NotMapped]
        public IDictionary<string, BindingFlags> MyProperty3 { get; set; }

        [NotMapped]
        public IList<MyClass<string, IList<bool>>> MyProperty4 { get; set; }
    }

    public class MyClass<T, V>
    {

    }
}
