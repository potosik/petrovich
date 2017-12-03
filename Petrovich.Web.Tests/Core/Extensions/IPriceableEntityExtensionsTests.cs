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
            var entity = new TestEntity() { Price = null };
            var result = entity.GetPriceInformation(PriceCalculationTypeBusiness.ByDailyPrice);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void GetPriceInformation_WhenPriceNotNull_ShouldReturnPriceString()
        {
            var entity = new TestEntity() { Price = 12.3f };
            var result = entity.GetPriceInformation(PriceCalculationTypeBusiness.ByDailyPrice);

            Assert.NotNull(result);
            Assert.Equal("12,30 BYN ОСЦ", result);
        }

        private class TestEntity : IPriceableEntityModel
        {
            public double? Price { get; set; }
        }
    }
}
