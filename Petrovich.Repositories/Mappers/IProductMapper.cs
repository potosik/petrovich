using Petrovich.Business.Models;
using Petrovich.Context.Entities;
using System.Collections.Generic;

namespace Petrovich.Repositories.Mappers
{
    public interface IProductMapper
    {
        ProductModel ToProductModel(Product product);
        ProductModelCollection ToProductModelCollection(IEnumerable<Product> products);
        Product ToContextProduct(ProductModel productModel);
    }
}
