using Petrovich.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
