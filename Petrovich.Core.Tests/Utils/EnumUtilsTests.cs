using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Petrovich.Core.Tests.Utils
{
    public class EnumUtilsTests
    {
        [Fact]
        public void GetValues_ShouldReturnIEnumerableOfValues()
        {
            var result = EnumUtils.GetValues<TestEnum>().ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal(TestEnum.First, result[0]);
            Assert.Equal(TestEnum.Second, result[1]);
            Assert.Equal(TestEnum.Third, result[2]);
        }

        [Fact]
        public void GetValuesStrings_ShouldReturnIEnumerableOfValuesStrings()
        {
            var result = EnumUtils.GetValuesStrings<TestEnum>().ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal("First", result[0]);
            Assert.Equal("Second", result[1]);
            Assert.Equal("Third", result[2]);
        }

        [Fact]
        public void GetValues_WithEmptyEnum_ShouldReturnIEnumerableOfValues()
        {
            var result = EnumUtils.GetValues<EmptyEnum>().ToArray();

            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void GetValuesStrings_WithEmptyEnum_ShouldReturnIEnumerableOfValuesStrings()
        {
            var result = EnumUtils.GetValuesStrings<EmptyEnum>().ToArray();

            Assert.Equal(0, result.Length);
        }

        private enum TestEnum
        {
            First,
            Second,
            Third,
        }

        private enum EmptyEnum
        {
        }
    }
}
