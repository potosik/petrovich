using System.Collections.Generic;
using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Linq;

namespace Petrovich.Repositories.Mappers.Concrete
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryModel ToCategoryModel(Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new CategoryModel()
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                InventoryPart = category.InventoryPart,
                Price = category.BasePrice,
                PriceType = EnumMapper.Map<Context.Enumerations.PriceType, Business.Models.Enumerations.PriceTypeBusiness>(category.PriceType),
                BranchId = category.BranchId,
                BranchTitle = category.Branch?.Title,
                BranchInventoryPart = category.Branch?.InventoryPart,
                
                Created = category.Created,
                CreatedBy = category.CreatedBy,
                Modified = category.Modified,
                ModifiedBy = category.ModifiedBy,
            };
        }

        public CategoryModelCollection ToCategoryModelCollection(IEnumerable<Category> categories)
        {
            return new CategoryModelCollection(categories.Select(item => ToCategoryModel(item)));
        }

        public Category ToContextCategory(CategoryModel categoryModel)
        {
            return new Category()
            {
                CategoryId = categoryModel.CategoryId,
                Title = categoryModel.Title,
                InventoryPart = categoryModel.InventoryPart,
                BasePrice = categoryModel.Price,
                PriceType = EnumMapper.Map<Business.Models.Enumerations.PriceTypeBusiness, Context.Enumerations.PriceType>(categoryModel.PriceType),
                BranchId = categoryModel.BranchId,

                Created = categoryModel.Created,
                CreatedBy = categoryModel.CreatedBy,
                Modified = categoryModel.Modified,
                ModifiedBy = categoryModel.ModifiedBy,
            };
        }
    }
}
