using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<IrregularVerb> IrregularVerbs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }

        //https://stackoverflow.com/questions/7641552/overriding-savechanges-and-setting-modifieddate-but-how-do-i-set-modifiedby
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            var now = DateTime.Now;
            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                        entity.DateCreated = now;
                    else
                        Entry(entity).Property(x => x.DateCreated).IsModified = false;

                    entity.DateModified = now;
                }
            }

            //return base.SaveChanges();

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}