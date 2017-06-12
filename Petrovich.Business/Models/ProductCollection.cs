using System.Collections.Generic;

namespace Petrovich.Business.Models
{
    public class ProductCollection : List<Product>
    {
        public int TotalCount { get; set; }

        public ProductCollection()
        {
        }

        public ProductCollection(IEnumerable<Product> products)
            : base(products)
        {
        }
    }
}
