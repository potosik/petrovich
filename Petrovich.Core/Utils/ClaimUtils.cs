using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
