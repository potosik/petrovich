using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        Task<TEntity> FindAsync(int id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(int id);
    }
}
