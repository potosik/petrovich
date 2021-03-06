﻿using Petrovich.Context;
using Petrovich.Context.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<bool> IsExistsForCategoryAsync(Guid categoryId)
        {
            return await context.Products.Where(item => item.CategoryId == categoryId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsExistsForGroupAsync(Guid groupId)
        {
            return await context.Products.Where(item => item.GroupId.HasValue && item.GroupId.Value == groupId).AnyAsync().ConfigureAwait(false);
        }

        public async Task<IList<int>> ListUsedInventoryPartsByCategoryAsync(Guid categoryId)
        {
            return await context.Products.Where(item => item.CategoryId == categoryId && item.GroupId == null).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<int>> ListUsedInventoryPartsByGroupAsync(Guid groupId)
        {
            return await context.Products.Where(item => item.GroupId == groupId).Select(item => item.InventoryPart).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> SearchFastAsync(string query, int count)
        {
            var q = context.Products.Where(item => item.Title.Contains(query)).OrderByDescending(item => item.Modified).Take(count);
            return await context.Products.Where(item => item.Title.Contains(query)).Take(count).ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> SearchFastCountAsync(string query)
        {
            return await context.Products.Where(item => item.Title.IndexOf(query) > -1).CountAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> ListByCategoryIdAsync(Guid categoryId)
        {
            return await context.Products.Where(item => item.CategoryId == categoryId && item.GroupId == null).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> ListByGroupIdAsync(Guid groupId)
        {
            return await context.Products.Where(item => item.GroupId == groupId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> ListAsync(IEnumerable<Guid> productIds)
        {
            return await context.Products.Where(item => productIds.Contains(item.ProductId)).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> ListAsync(string filter, int pageIndex, int pageSize)
        {
            var query = context.Products.AsQueryable();
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(item => item.Title.Contains(filter));
            }
            return await query.OrderByDescending(item => item.Created).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> ListCountAsync(string filter)
        {
            var query = context.Products.AsQueryable();
            if (!String.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(item => item.Title.Contains(filter));
            }
            return await query.CountAsync().ConfigureAwait(false);
        }
    }
}
