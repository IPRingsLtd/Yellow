using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace _360Appraisal.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection") { }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }

        private void BeforeSave()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Base)
                {
                    var entity = (Base)entry.Entity;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.Now;
                        entity.UpdatedAt = DateTime.Now;
                        entity.ActiveFlag = true;
                        entity.Key = Guid.NewGuid().ToString();
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = DateTime.Now;
                    }
                }
            } 
        }
        public override int SaveChanges()
        {
            BeforeSave();
            return base.SaveChanges();
        }
        public async override Task<int> SaveChangesAsync()
        {
            BeforeSave();
            return await base.SaveChangesAsync();
        }
    }
    public class AppContextInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    { 

    }
}