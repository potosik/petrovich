using Petrovich.Business.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Web.Core.Extensions;
using System.Resources;

namespace Petrovich.Web.Tests.Core.Extensions
{
    public class IPriceableEntityExtensionsTests
    {
        [Fact]
        public void GetPriceInformation_WhenPriceIsNull_ShouldReturnPriceNotAvailableString()
        {
            var entity = new TestEntity() { Price = null, PriceType = PriceTypeBusiness.Day };
            var result = entity.GetPriceInformation();

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void GetPriceInformation_WhenPriceTypeIsNull_ShouldReturnPriceNotAvailableString()
        {
            var entity = new TestEntity() { Price = 1f, PriceType = null };
            var result = entity.GetPriceInformation();

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void GetPriceInformation_WhenPriceAndPriceTypeIsNull_ShouldReturnPriceNotAvailableString()
        {
            var entity = new TestEntity() { Price = null, PriceType = null };
            var result = entity.GetPriceInformation();

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void GetPriceInformation_WhenPriceAndPriceTypeNotNull_ShouldReturnPriceNotAvailableString()
        {
            var entity = new TestEntity() { Price = 12.3f, PriceType = PriceTypeBusiness.Month };
            var result = entity.GetPriceInformation();

            Assert.NotNull(result);
            Assert.Equal("12,30 BYN / месяц", result);
        }

        private class TestEntity : IPriceableEntityModel
        {
            public double? Price { get; set; }
            public PriceTypeBusiness? PriceType { get; set; }
        }
    }
}
