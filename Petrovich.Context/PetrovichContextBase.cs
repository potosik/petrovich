using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Petrovich.Context.Entities.Base;
using System.Web;
using System.Threading;

namespace Petrovich.Context
{
    public class PetrovichContextBase : DbContext, IPetrovichContextBase
    {
        public PetrovichContextBase()
            : base("databaseConnectionString")
        {
        }
        
        DbSet<TEntity> IPetrovichContextBase.Set<TEntity>()
        {
            return (this as DbContext).Set<TEntity>();
        }

        void IPetrovichContextBase.SaveChanges()
        {
            UpdateChangesTrackingInformation();
            (this as DbContext).SaveChanges();
        }

        async Task IPetrovichContextBase.SaveChangesAsync()
        {
            UpdateChangesTrackingInformation();
            await (this as DbContext).SaveChangesAsync();
        }

        async Task IPetrovichContextBase.SaveChangesAsync(CancellationToken cancellationToken)
        {
            UpdateChangesTrackingInformation();
            await (this as DbContext).SaveChangesAsync(cancellationToken);
        }

        private void UpdateChangesTrackingInformation()
        {
            var user = HttpContext.Current?.User?.Identity?.Name;
            var entries = ChangeTracker
                .Entries()
                .Where(item => item.Entity is IChangeTrackableEntity && (item.State == EntityState.Added || item.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((IChangeTrackableEntity)entry.Entity).Created = DateTime.UtcNow;
                    ((IChangeTrackableEntity)entry.Entity).CreatedBy = user;
                }

                ((IChangeTrackableEntity)entry.Entity).Modified = DateTime.UtcNow;
                ((IChangeTrackableEntity)entry.Entity).ModifiedBy = user;
            }
        }
    }
}
