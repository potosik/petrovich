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
        Task<ProductCollection> ListAsync(int pageIndex, int pageSize);
        Task<Product> CreateAsync(Product product);
        Task<Product> FindAsync(Guid id);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<ProductCollection> SearchFastAsync(string query, int count);
        Task<ProductCollection> ListByCategoryIdAsync(Guid categoryId);
        Task<ProductCollection> ListByGroupIdAsync(Guid groupId);
    }
}
