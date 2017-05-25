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
        private DbContext context;
        
        public BaseRepostory(IPetrovichContext context)
        {
            this.context = context as DbContext ?? throw new ArgumentOutOfRangeException(nameof(BaseRepostory<TEntity>.context));
        }

        public IDbSet<TEntity> Entities
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        public abstract Task<TEntity> FindByIdAsync(int id);
        
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Entities.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            Entities.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
