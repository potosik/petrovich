using Petrovich.Context;
using Petrovich.Context.Entities.Base;
using System;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public abstract class BaseRepostory<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly IPetrovichContext context;

        public BaseRepostory(IPetrovichContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public abstract Task<TEntity> FindAsync(int id);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await FindAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }
    }
}
