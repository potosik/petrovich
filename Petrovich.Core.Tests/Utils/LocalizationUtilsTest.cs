using Petrovich.Core.Utils;
using Xunit;

namespace Petrovich.Core.Tests.Utils
{
    public class LocalizationUtilsTest
    {
        private const string TestStringKey = "TestString";
        private const string TestStringValue = "TestString";
        private const string NonExistingStringKey = "NonExistingString###";

        [Fact]
        public void GetString_ShouldReturnCorrectStringByKey()
        {
            var result = LocalizationUtils.GetString(TestStringKey);

            Assert.Equal(TestStringValue, result);
        }

        [Fact]
        public void GetString_WithNonExistingStringKey_ShouldReturnNull()
        {
            var result = LocalizationUtils.GetString(NonExistingStringKey);

            Assert.Null(result);
        }
    }
}
