using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Core.Tests.Utils
{
    public class DateTimeUtilsTests
    {
        [Fact]
        public void CreatePurchaseDate_WhenYeasHasNoValue_ShouldReturnDashOnly()
        {
            var result = DateTimeUtils.CreatePurchaseDate(null, 1);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void CreatePurchaseDate_WhenMonthHasNoValue_ShouldReturnDashOnly()
        {
            var result = DateTimeUtils.CreatePurchaseDate(1, null);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void CreatePurchaseDate_WhenYearAndMonthHasNoValue_ShouldReturnDashOnly()
        {
            var result = DateTimeUtils.CreatePurchaseDate(null, null);

            Assert.NotNull(result);
            Assert.Equal("-", result);
        }

        [Fact]
        public void CreatePurchaseDate_WhenYearAndMonthHasValue_ShouldReturnFormatedDate()
        {
            var result = DateTimeUtils.CreatePurchaseDate(2017, 1);

            Assert.NotNull(result);
            Assert.Equal("Январь 2017", result);
        }
    }
}
