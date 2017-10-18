using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class CategoryModelCollection : List<CategoryModel>
    {
        public int TotalCount { get; set; }

        public CategoryModelCollection()
        {
        }

        public CategoryModelCollection(IEnumerable<CategoryModel> categories)
            : base(categories)
        {
        }
    }
}
