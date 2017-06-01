using Petrovich.Business.Data;
using System;

namespace Petrovich.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDataSource dataSource;

        public CategoryService(ICategoryDataSource dataSource)
        {
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }
    }
}
