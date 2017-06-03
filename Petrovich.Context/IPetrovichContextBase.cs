using Petrovich.Context.Entities.Base;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Petrovich.Context
{
    public interface IPetrovichContextBase : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        void SaveChanges();
        Task SaveChangesAsync();
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
