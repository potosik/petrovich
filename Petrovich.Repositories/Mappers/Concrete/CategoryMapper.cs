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
                PriceCalculationType = EnumMapper.Map<Context.Enumerations.PriceCalculationType, Business.Models.Enumerations.PriceCalculationTypeBusiness>(category.PriceCalculationType),
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
                PriceCalculationType = EnumMapper.Map<Business.Models.Enumerations.PriceCalculationTypeBusiness, Context.Enumerations.PriceCalculationType>(categoryModel.PriceCalculationType),
                BranchId = categoryModel.BranchId,

                Created = categoryModel.Created,
                CreatedBy = categoryModel.CreatedBy,
                Modified = categoryModel.Modified,
                ModifiedBy = categoryModel.ModifiedBy,
            };
        }
    }
}
