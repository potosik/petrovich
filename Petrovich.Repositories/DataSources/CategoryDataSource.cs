using Petrovich.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Business.Models;
using Petrovich.DataSource.Mappers;
using System.Data.Entity.Core;
using Petrovich.Business.Exceptions;
using Petrovich.Core;
using Petrovich.Context.Enumerations;

namespace Petrovich.Repositories.DataSources
{
    public class CategoryDataSource : ICategoryDataSource
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryMapper categoryMapper;

        public CategoryDataSource(ICategoryRepository categoryRepository, ICategoryMapper categoryMapper)
        {
            this.categoryRepository = categoryRepository;
            this.categoryMapper = categoryMapper;
        }

        public async Task<CategoryModelCollection> ListAsync(int pageIndex, int pageSize)
        {
            try
            {
                var categories = await categoryRepository.ListAsync(pageIndex, pageSize);
                var count = await categoryRepository.ListCountAsync();
                var collection = categoryMapper.ToCategoryModelCollection(categories);
                collection.TotalCount = count;
                return collection;
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
        
        public async Task<CategoryModel> CreateAsync(CategoryModel category)
        {
            try
            {
                var contextCategory = categoryMapper.ToContextCategory(category);
                var newCategory = await categoryRepository.CreateAsync(contextCategory);
                return categoryMapper.ToCategoryModel(newCategory);
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

        public async Task<CategoryModel> FindAsync(Guid id)
        {
            try
            {
                var category = await categoryRepository.FindAsync(id);
                return categoryMapper.ToCategoryModel(category);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<CategoryModel> UpdateAsync(CategoryModel category)
        {
            try
            {
                var targetCategory = await categoryRepository.FindAsync(category.CategoryId);

                targetCategory.Title = category.Title;
                targetCategory.InventoryPart = category.InventoryPart;
                targetCategory.BasePrice = category.Price;
                targetCategory.PriceCalculationType = EnumMapper.Map<Business.Models.Enumerations.PriceCalculationTypeBusiness, PriceCalculationType>(category.PriceCalculationType);
                targetCategory.BranchId = category.BranchId;

                await categoryRepository.UpdateAsync(targetCategory);
                return categoryMapper.ToCategoryModel(targetCategory);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task DeleteAsync(CategoryModel category)
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

        public async Task<CategoryModelCollection> ListByBranchIdAsync(Guid branchId)
        {
            try
            {
                var categories = await categoryRepository.ListByBranchIdAsync(branchId);
                return categoryMapper.ToCategoryModelCollection(categories);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }

        public async Task<CategoryModelCollection> ListAllAsync()
        {
            try
            {
                var categories = await categoryRepository.ListAllAsync();
                return categoryMapper.ToCategoryModelCollection(categories);
            }
            catch (EntityException ex)
            {
                throw new DatabaseOperationException(ex);
            }
        }
    }
}
