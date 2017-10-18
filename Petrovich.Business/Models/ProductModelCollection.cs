using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class ProductModelCollection : List<ProductModel>
    {
        public int TotalCount { get; set; }

        public ProductModelCollection()
        {
        }

        public ProductModelCollection(IEnumerable<ProductModel> products)
            : base(products)
        {
        }
    }
}
