using Microsoft.AspNet.Identity.EntityFramework;
using Petrovich.Core;
using System.Collections.Generic;
using System.Linq;

namespace Petrovich.Web.Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUserName(this ICollection<IdentityUserClaim> claims)
        {
            return claims.FirstOrDefault(item => item.ClaimType == PetrovichClaims.UserName.ToString())?.ClaimValue;
        }
    }
}