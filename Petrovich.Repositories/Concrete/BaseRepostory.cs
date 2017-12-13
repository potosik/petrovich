using Petrovich.Context;
using Petrovich.Context.Entities.Base;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Petrovich.Repositories.Concrete
{
    public abstract class BaseRepostory<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        protected readonly IPetrovichContext context;

        public BaseRepostory(IPetrovichContext context)
        {
            this.context = context;
        }
        
        public abstract Task<TEntity> FindAsync(Guid id);

        public virtual async Task<IList<TEntity>> ListAllAsync()
        {
            return await ListAsync(0, 0);
        }

        public virtual async Task<IList<TEntity>> ListAsync(int pageIndex, int pageSize)
        {
            var items = context.Set<TEntity>();
            if (pageSize == 0)
            {
                return await items.ToListAsync().ConfigureAwait(false);
            }

            return await items.OrderByDescending(item => item.Created).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<int> ListCountAsync()
        {
            return await context.Set<TEntity>().CountAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task DeleteByIdAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }
    }
}
