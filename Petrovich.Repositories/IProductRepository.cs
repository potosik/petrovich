using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<bool> IsExistsForGroupAsync(Guid groupId);
        Task<IList<int>> ListUsedInventoryPartsAsync(Guid categoryId);
    }
}
