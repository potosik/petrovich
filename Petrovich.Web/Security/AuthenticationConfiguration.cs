using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Petrovich.Core;
using Petrovich.Core.Utils;
using Petrovich.Web.Models;
using Petrovich.Web.Security.Identity;
using System;
using System.Collections.Generic;

namespace Petrovich.Web.Security
{
    public static class AuthenticationConfiguration
    {
        internal static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromHours(8),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }

        internal static void CreateDefaultArtifacts(IAppBuilder app)
        {
            var context = new ApplicationDbContext();

            CreateDefaultUser(context);
        }

        private static void CreateDefaultUser(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var defaultUser = userManager.FindByEmail(Defaults.User.Email);
            if (defaultUser == null)
            {
                RegisterDefaultUser(userManager);
            }
        }

        private static void RegisterDefaultUser(ApplicationUserManager userManager)
        {
            var user = new ApplicationUser()
            {
                Email = Defaults.User.Email,
                UserName = Defaults.User.Email,
            };

            var userCreationResult = userManager.Create(user, Defaults.User.Password);
            if (userCreationResult.Succeeded)
            {
                AddDefaultUserClaims(user, userManager);
            }
            else
            {
                GenerateError("Error creating default user", userCreationResult.Errors);
            }
        }

        private static void AddDefaultUserClaims(ApplicationUser user, ApplicationUserManager userManager)
        {
            foreach (var claim in EnumUtils.GetValues<PetrovichClaims>())
            {
                var claimString = claim.ToString();
                user.Claims.Add(new IdentityUserClaim() { ClaimType = claimString, ClaimValue = claimString });
            }

            userManager.Update(user);
        }
        
        private static void GenerateError(string mainMessage, IEnumerable<string> innerErrors)
        {
            throw new InvalidOperationException(mainMessage, new Exception(String.Join("|", innerErrors)));
        }
    }
}