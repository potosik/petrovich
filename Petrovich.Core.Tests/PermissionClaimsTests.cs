using Petrovich.Core.Utils;
using Xunit;

namespace Petrovich.Core.Tests
{
    public class PermissionClaimsTests
    {
        [Fact]
        public void AllClaimsHasLocalization()
        {
            var claims = EnumUtils.GetValuesStrings<PermissionClaims>();

            foreach (var claim in claims)
            {
                Assert.NotNull(LocalizationUtils.GetString(claim));
            }
        }
    }
}
