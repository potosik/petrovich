﻿using Petrovich.Core;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Security.Attributes
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionClaims[] Claims { get; set; } = new PermissionClaims[0];

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return false;
            }
            
            foreach (var claim in Claims)
            {
                var claimString = claim.ToString();
                if (claimsIdentity.HasClaim(claimString, claimString))
                {
                    return base.AuthorizeCore(httpContext);
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}