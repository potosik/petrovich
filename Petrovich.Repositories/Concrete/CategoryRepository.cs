using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public class CategoryRepository : BaseRepostory<Category>, ICategoryRepository
    {
        public CategoryRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Category> FindAsync(Guid id)
        {
            return await context.Categories.FirstOrDefaultAsync(item => item.CategoryId == id).ConfigureAwait(false);
        }
    }
}
