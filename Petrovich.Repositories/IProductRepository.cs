﻿using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petrovich.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<bool> IsExistsForCategoryAsync(Guid categoryId);
        Task<bool> IsExistsForGroupAsync(Guid groupId);
        Task<IList<int>> ListUsedInventoryPartsByCategoryAsync(Guid categoryId);
        Task<IList<int>> ListUsedInventoryPartsByGroupAsync(Guid groupId);
        Task<IList<Product>> SearchFastAsync(string query, int count);
        Task<int> SearchFastCountAsync(string query);
        Task<IList<Product>> ListByCategoryIdAsync(Guid categoryId);
        Task<IList<Product>> ListByGroupIdAsync(Guid groupId);
        Task<IList<Product>> ListAsync(IEnumerable<Guid> productIds);
        Task<IList<Product>> ListAsync(string filter, int pageIndex, int pageSize);
        Task<int> ListCountAsync(string filter);
    }
}
