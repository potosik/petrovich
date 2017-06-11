using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.Repositories.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;
using Petrovich.Core;

namespace Petrovich.Repositories.DataSources
{
    public class CategoryDataSource : ICategoryDataSource
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryMapper categoryMapper;

        public CategoryDataSource(ICategoryRepository categoryRepository, ICategoryMapper categoryMapper)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.categoryMapper = categoryMapper ?? throw new ArgumentNullException(nameof(categoryMapper));
        }

        public async Task<CategoryCollection> ListAsync()
        {
            try
            {
                var categories = await categoryRepository.ListAllAsync();
                return categoryMapper.ToBusinessEntityCollection(categories);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Category> FindByInventoryPartAsync(int inventoryPart, Guid branchId)
        {
            try
            {
                var category = await categoryRepository.FindByInventoryPartAsync(inventoryPart, branchId);
                return categoryMapper.ToBusinessEntity(category);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Category> CreateAsync(Category category)
        {
            try
            {
                var contextCategory = categoryMapper.ToContextEntity(category);
                var newCategory = await categoryRepository.CreateAsync(contextCategory);
                return categoryMapper.ToBusinessEntity(newCategory);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<int?> GetNewInventoryNumberAsync(Guid branchId)
        {
            try
            {
                var usedInventoryPartNumbers = await categoryRepository.ListUsedInventoryPartsAsync(branchId);
                if (usedInventoryPartNumbers.Count == Constants.CategoryInventoryPartMaxCount)
                {
                    return null;
                }

                for (int i = Constants.CategoryInventoryPartMinValue; i < Constants.CategoryInventoryPartMaxValue; i++)
                {
                    if (!usedInventoryPartNumbers.Contains(i))
                        return i;
                }

                return null;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Category> FindAsync(Guid id)
        {
            try
            {
                var category = await categoryRepository.FindAsync(id);
                return categoryMapper.ToBusinessEntity(category);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            try
            {
                var targetCategory = await categoryRepository.FindAsync(category.CategoryId);

                targetCategory.Title = category.Title;
                targetCategory.InventoryPart = category.InventoryPart;
                targetCategory.BranchId = category.BranchId;

                await categoryRepository.UpdateAsync(targetCategory);
                return categoryMapper.ToBusinessEntity(targetCategory);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(Category category)
        {
            try
            {
                var targetCategory = await categoryRepository.FindAsync(category.CategoryId);
                await categoryRepository.DeleteAsync(targetCategory);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<bool> IsExistsForBranchAsync(Guid branchId)
        {
            try
            {
                return await categoryRepository.IsExistsForBranchAsync(branchId);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<CategoryCollection> ListByBranchIdAsync(Guid branchId)
        {
            try
            {
                var categories = await categoryRepository.ListByBranchIdAsync(branchId);
                return categoryMapper.ToBusinessEntityCollection(categories);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
