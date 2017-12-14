using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.DataSource.Mappers
{
    public interface ICategoryMapper
    {
        CategoryModel ToCategoryModel(Category category);
        CategoryModelCollection ToCategoryModelCollection(IEnumerable<Category> categories);
        Category ToContextCategory(CategoryModel categoryModel);
    }
}
