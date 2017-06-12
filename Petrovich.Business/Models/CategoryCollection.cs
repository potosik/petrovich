using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class CategoryCollection : List<Category>
    {
        public int TotalCount { get; set; }

        public CategoryCollection()
        {
        }

        public CategoryCollection(IEnumerable<Category> categories)
            : base(categories)
        {
        }
    }
}
