using Petrovich.Core;
using System.Security.Claims;
using System.Security.Principal;

namespace Petrovich.Web.Core.Extensions
{
    public static class IIdentityExtensions
    {
        public static string GetUserName(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst(PetrovichClaims.UserName.ToString())?.Value;
        }

        public static bool HasClaim(this IIdentity identity, PermissionClaims claim)
        {
            return ((ClaimsIdentity)identity).HasClaim(claim.ToString(), claim.ToString());
        }
    }
}