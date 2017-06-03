using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Concrete
{
    public class ProductRepository : BaseRepostory<Product>, IProductRepository
    {
        public ProductRepository(IPetrovichContext context)
            : base(context)
        {
        }

        public override async Task<Product> FindAsync(Guid id)
        {
            return await context.Products.FirstOrDefaultAsync(item => item.ProductId == id).ConfigureAwait(false);
        }
    }
}
