using Petrovich.Core.Utils;
using Xunit;

namespace Petrovich.Core.Tests
{
    public class PetrovichClaimsTests
    {
        [Fact]
        public void AllClaimsHasLocalization()
        {
            var claims = EnumUtils.GetValuesStrings<PetrovichClaims>();

            foreach (var claim in claims)
            {
                Assert.NotNull(LocalizationUtils.GetString(claim));
            }
        }
    }
}
