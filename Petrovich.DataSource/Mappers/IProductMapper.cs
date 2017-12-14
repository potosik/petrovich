using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.DataSource.Mappers
{
    public interface IProductMapper
    {
        ProductModel ToProductModel(Product product);
        ProductModelCollection ToProductModelCollection(IEnumerable<Product> products);
        Product ToContextProduct(ProductModel productModel);
    }
}
