using Petrovich.Core.Utils;
using Xunit;

namespace Petrovich.Core.Tests.Utils
{
    public class ClaimUtilsTests
    {
        [Fact]
        public void GetPublicClaims_NotReturnPowerAdminClaim()
        {
            var result = ClaimUtils.GetPublicClaims();

            Assert.DoesNotContain(PermissionClaims.PowerAdmin, result);
        }
    }
}
