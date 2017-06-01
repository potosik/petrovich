using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataSource dataSource;

        public ProductService(IProductDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
