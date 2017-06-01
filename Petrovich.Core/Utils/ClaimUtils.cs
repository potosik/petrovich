using System.Collections.Generic;
using System.Linq;

namespace Petrovich.Core.Utils
{
    public static class ClaimUtils
    {
        public static IList<PermissionClaims> GetPublicClaims()
        {
            return EnumUtils.GetValues<PermissionClaims>().Where(item => item != PermissionClaims.PowerAdmin).ToList();
        }
    }
}
