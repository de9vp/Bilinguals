using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilinguals.Models
{
    public class BilingualsDbContext : IdentityDbContext<ApplicationUser>
    {
        public BilingualsDbContext()
            : base("Bilinguals", throwIfV1Schema: false)
        {
        }

        public static BilingualsDbContext Create()
        {
            return new BilingualsDbContext();
        }
    }
}