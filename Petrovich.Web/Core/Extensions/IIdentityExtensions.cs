using Petrovich.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Petrovich.Web.Core.Extensions
{
    public static class IIdentityExtensions
    {
        public static string GetUserName(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst(PetrovichClaims.UserName.ToString())?.Value;
        }
    }
}