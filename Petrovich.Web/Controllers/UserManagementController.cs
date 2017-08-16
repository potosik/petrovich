using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Petrovich.Business.Exceptions;
using Petrovich.Business.Logging;
using Petrovich.Core;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Web.Core.Security.DbContext.Entities;
using Petrovich.Web.Models.UserManagement;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.UserManagement, PermissionClaims.PowerAdmin })]
    public class UserManagementController : IdentityController
    {
        public UserManagementController(ILoggingService logging)
            : base(logging)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Active()
        {
            var users = await UserManager.Users
                .Where(user => !user.LockoutEnabled && user.Email != Defaults.User.Email)
                .ToListAsync();

            var model = users.Select(user => ApplicationUserViewModel.Create(user));
            return View("UserList", model);
        }

        [HttpGet]
        public async Task<ActionResult> Deleted()
        {
            var users = await UserManager.Users
                .Where(user => user.LockoutEnabled)
                .ToListAsync();

            var model = users.Select(user => ApplicationUserViewModel.Create(user));
            return View("UserList", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ApplicationUserCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUserCreateViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await UpdateApplicationUser(userModel);
                if (result.Succeeded)
                {
                    await logger.LogNoneAsync("User successfully created.");
                    return RedirectToAction(PetrovichRoutes.UserManagement.Active);
                }

                await logger.LogInformationAsync("User wan not created.");
                AddErrors(result);
            }
            else
            {
                await logger.LogInvalidModelAsync(userModel.GetType());
            }

            return View(userModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                await logger.LogInformationAsync($"User '{id}' not found for edit.");
                return CreateNotFoundResponse();
            }

            var model = ApplicationUserEditViewModel.Create(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUserEditViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await UpdateApplicationUser(userModel);
                    if (result.Succeeded)
                    {
                        await logger.LogNoneAsync($"User '{userModel.Id}' successfully updated.");
                        return RedirectToAction(PetrovichRoutes.UserManagement.Active);
                    }

                    await logger.LogInformationAsync($"User '{userModel.Id}' update completed unsuccessfully.");
                    AddErrors(result);
                }
                catch (UserNotFoundException ex)
                {
                    await logger.LogErrorAsync($"User '{userModel.Id}' not found for update.", ex);
                    return CreateNotFoundResponse();
                }
            }
            else
            {
                await logger.LogInvalidModelAsync(userModel.GetType());
            }

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                await logger.LogErrorAsync($"Trying to delete user with invalid id ({id}).");
                return CreateBadRequestResponse();
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                await logger.LogErrorAsync($"User '{id}' not found for delete.");
                return CreateNotFoundResponse();
            }

            user.LockoutEnabled = true;
            await UserManager.UpdateAsync(user);

            await logger.LogNoneAsync($"User '{id}' successfully deleted.");
            return RedirectToAction(PetrovichRoutes.UserManagement.Active);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Restore(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                await logger.LogErrorAsync($"Trying to restore user with invalid id ({id}).");
                return CreateBadRequestResponse();
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                await logger.LogErrorAsync($"User '{id}' not found for restore.");
                return CreateNotFoundResponse();
            }

            user.LockoutEnabled = false;
            await UserManager.UpdateAsync(user);

            await logger.LogNoneAsync($"User '{id}' successfully restored.");
            return RedirectToAction(PetrovichRoutes.UserManagement.Active);
        }

        private async Task<IdentityResult> UpdateApplicationUser(IUpdateApplicationUserViewModel userModel)
        {
            ApplicationUser user = null;
            IdentityResult result = null;

            if (String.IsNullOrWhiteSpace(userModel.Id))
            {
                await logger.LogNoneAsync($"Attempting to create a new user '{userModel.Email}'.");
                user = new ApplicationUser { UserName = userModel.Email, Email = userModel.Email };
                result = await UserManager.CreateAsync(user, userModel.ConfirmPassword);
            }
            else
            {
                await logger.LogNoneAsync($"Attempting to update an existing user '{userModel.Email}'.");
                user = await UserManager.FindByIdAsync(userModel.Id.ToString());
                if (user == null)
                {
                    await logger.LogErrorAsync($"User {userModel.Id}/{userModel.Email} was not found for edit.");
                    throw new UserNotFoundException(userModel.Id.ToString());
                }

                user.Email = userModel.Email;
                user.UserName = userModel.Email;

                result = await UserManager.UpdateAsync(user);
                if (result.Succeeded && !String.IsNullOrWhiteSpace(userModel.ConfirmPassword))
                {
                    await logger.LogNoneAsync($"Attempting to update password for existing user '{userModel.Email}'.");
                    var passwordChangeToken = await UserManager.GeneratePasswordResetTokenAsync(userModel.Id);
                    result = await UserManager.ResetPasswordAsync(user.Id, passwordChangeToken, userModel.ConfirmPassword);
                    if (result.Succeeded)
                    {
                        await logger.LogNoneAsync($"Password updated for '{userModel.Email}'.");
                    }
                    else
                    {
                        await logger.LogInformationAsync($"Password was not updated for '{userModel.Email}'.");
                    }
                }
            }

            if (result.Succeeded)
            {
                await logger.LogNoneAsync($"Attempting to update claims for user '{userModel.Email}'.");
                UpdateUserClaims(userModel, user);
                result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await logger.LogNoneAsync($"Claims updated for '{userModel.Email}'.");
                }
                else
                {
                    await logger.LogInformationAsync($"Claims was not updated for '{userModel.Email}'.");
                }
            }

            return result;
        }

        private void UpdateUserClaims(IUpdateApplicationUserViewModel userModel, ApplicationUser user)
        {
            for (int i = user.Claims.Count - 1; i >= 0; i--)
            {
                var identityClaim = user.Claims.ElementAt(i);
                UserManager.RemoveClaim(user.Id, new System.Security.Claims.Claim(identityClaim.ClaimType, identityClaim.ClaimValue));
            }

            user.Claims.Add(new IdentityUserClaim() { ClaimType = PetrovichClaims.UserName.ToString(), ClaimValue = userModel.UserName });

            foreach (var claim in userModel.Claims)
            {
                var parsedClaim = PermissionClaims.PowerAdmin;
                if (Enum.TryParse(claim, out parsedClaim))
                {
                    user.Claims.Add(new IdentityUserClaim()
                    {
                        ClaimType = claim,
                        ClaimValue = claim,
                    });
                }
            }
        }
    }
}