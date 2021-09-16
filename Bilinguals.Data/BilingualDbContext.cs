using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        //warning: after add model
        public DbSet<Dialog> Dialogs { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
        public DbSet<UserDialog> UserDialogs { get; set; }
        public DbSet<UserSentence> UserSentences { get; set; }
        public DbSet<UserLearning> UserLearnings { get; set; }

        
    }
}