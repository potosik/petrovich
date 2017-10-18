using Petrovich.Business.Models;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Web.Tests.Core.Extensions
{
    public class ProductExtensionsTests
    {
        private Product product = new Product()
        {
            Price = 12.3f,
            PriceType = Business.Models.Enumerations.PriceType.Month,

            Group = new Group()
            {
                Price = 45.6f,
                PriceType = Business.Models.Enumerations.PriceType.Day,
            },

            Category = new Category()
            {
                Price = 78.9f,
                PriceType = Business.Models.Enumerations.PriceType.Week,
            }
        };

        [Fact]
        public void GetHierarchicalPrice_WhenProductHasPrice_ShouldReturnProductPrice()
        {
            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("12,30 BYN / месяц", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupHasPrice_ShouldReturnGroupPrice()
        {
            product.Price = null;
            product.PriceType = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("45,60 BYN / день", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupNoCategoryHasPrice_ShouldReturnCategoryPrice()
        {
            product.Price = null;
            product.PriceType = null;
            product.Group.Price = null;
            product.Group.PriceType = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("78,90 BYN / неделя", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupNullCategoryHasPrice_ShouldReturnCategoryPrice()
        {
            product.Price = null;
            product.PriceType = null;
            product.Group = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("78,90 BYN / неделя", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenNoAnyPrice_ShouldReturnPriceFormatNotAvailableString()
        {
            product.Price = null;
            product.PriceType = null;
            product.Group = null;
            product.Category.Price = null;
            product.Category.PriceType = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }
    }
}
