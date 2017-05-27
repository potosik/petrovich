using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Utils
{
    public static class ClaimUtils
    {
        public static IList<PetrovichClaims> GetPublicClaims()
        {
            return EnumUtils.GetValues<PetrovichClaims>().Where(item => item != PetrovichClaims.PowerAdmin).ToList();
        }
    }
}
