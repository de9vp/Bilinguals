using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilinguals.Data
{
    public class BilingualDbContext : IdentityDbContext<ApplicationUser>, IBilingualDbContext
    {
        public BilingualDbContext()
            : base("Bilinguals", throwIfV1Schema: false)
        {
        }

        public static BilingualDbContext Create()
        {
            return new BilingualDbContext();
        }
    }
}