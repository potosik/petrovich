using Petrovich.Context.Entities;
using Petrovich.Context.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
