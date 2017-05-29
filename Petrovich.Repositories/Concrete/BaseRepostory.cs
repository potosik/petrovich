using Petrovich.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public abstract class BaseRepostory<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly IPetrovichContext context;
        private readonly DbContext dbContext;

        public BaseRepostory(IPetrovichContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            dbContext = context as DbContext;
        }
        
        public abstract Task<TEntity> FindAsync(int id);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var dbContext = context as DbContext;
            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
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
