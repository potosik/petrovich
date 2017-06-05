﻿using Petrovich.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business
{
    public interface IProductService
    {
        Task<ProductCollection> ListAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> FindAsync(Guid id);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}