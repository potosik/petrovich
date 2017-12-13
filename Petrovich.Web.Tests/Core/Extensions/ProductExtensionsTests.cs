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
        private ProductModel product = new ProductModel()
        {
            Price = 12.3f,

            Group = new GroupModel()
            {
                Price = 45.6f,
            },

            Category = new CategoryModel()
            {
                Price = 78.9f,
            }
        };

        [Fact]
        public void GetHierarchicalPrice_WhenProductHasPrice_ShouldReturnProductPrice()
        {
            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("12,30 BYN ОСЦ", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupHasPrice_ShouldReturnGroupPrice()
        {
            product.Price = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("45,60 BYN ОСЦ", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupNoCategoryHasPrice_ShouldReturnCategoryPrice()
        {
            product.Price = null;
            product.Group.Price = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("78,90 BYN ОСЦ", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenProductNoGroupNullCategoryHasPrice_ShouldReturnCategoryPrice()
        {
            product.Price = null;
            product.Group = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("78,90 BYN ОСЦ", result);
        }

        [Fact]
        public void GetHierarchicalPrice_WhenNoAnyPrice_ShouldReturnPriceFormatNotAvailableString()
        {
            product.Price = null;
            product.Group = null;
            product.Category.Price = null;

            var result = product.GetHierarchicalPrice();

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }
    }
}
