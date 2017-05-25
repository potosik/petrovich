using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        IDbSet<TEntity> Entities { get; }

        Task<TEntity> FindByIdAsync(int id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(int id);

        Task SaveChangesAsync();
    }
}
