using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business
{
    public interface IProductService
    {
        Task<ProductModelCollection> ListAsync(int pageIndex, int pageSize);
        Task<ProductModel> CreateAsync(ProductModel product);
        Task<ProductModel> FindAsync(Guid id);
        Task<ProductModel> UpdateAsync(ProductModel product);
        Task DeleteAsync(Guid id);
        Task<ProductModelCollection> SearchFastAsync(string query, int count);
        Task<ProductModelCollection> ListByCategoryIdAsync(Guid categoryId);
        Task<ProductModelCollection> ListByGroupIdAsync(Guid groupId);
        Task<ProductModelCollection> ListAsync(IEnumerable<Guid> productIds);
    }
}
