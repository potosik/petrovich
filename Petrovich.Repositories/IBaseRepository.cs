using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        Task<TEntity> FindAsync(Guid id);
        Task<IList<TEntity>> ListAllAsync();
        Task<IList<TEntity>> ListAsync(int pageIndex, int pageSize);
        Task<int> ListCountAsync();

        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(Guid id);
    }
}
