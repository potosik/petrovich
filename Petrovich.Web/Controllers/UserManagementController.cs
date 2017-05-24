using Petrovich.Core;
using Petrovich.Core.Navigation;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Web.Core.Security.DbContext.Entities;
using Petrovich.Web.Models.UserManagement;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [ClaimsAuthorize(Claims = new[] { PetrovichClaims.UserManagement })]
    public class UserManagementController : IdentityController
    {
        [HttpGet]
        public async Task<ActionResult> Active()
        {
            var users = await UserManager.Users
                .Where(user => !user.LockoutEnabled)
                .ToListAsync();

            var model = users.Select(user => ApplicationUserModel.Create(user));
            return View("UserList", model);
        }

        [HttpGet]
        public async Task<ActionResult> Deleted()
        {
            var users = await UserManager.Users
                .Where(user => user.LockoutEnabled)
                .ToListAsync();

            var model = users.Select(user => ApplicationUserModel.Create(user));
            return View("UserList", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateApplicationUserModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateApplicationUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = userModel.Email, Email = userModel.Email };
                var result = await UserManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(PetrovichRoutes.UserManagement.Active);
                }

                AddErrors(result);
            }

            return View(userModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return CreateNotFoundResponse();
            }

            var model = EditApplicationUserModel.Create(user);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditApplicationUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(userModel.Id.ToString());
                if (user == null)
                {
                    return CreateNotFoundResponse();
                }

                user.Email = userModel.Email;
                user.UserName = userModel.Email;
                await UserManager.UpdateAsync(user);
                
                return RedirectToAction(PetrovichRoutes.UserManagement.Active);
            }
            
            return View(userModel);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return CreateBadRequestResponse();
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return CreateNotFoundResponse();
            }

            user.LockoutEnabled = true;
            await UserManager.UpdateAsync(user);
            return RedirectToAction(PetrovichRoutes.UserManagement.Deleted);
        }

        [HttpPost]
        public async Task<ActionResult> Restore(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return CreateBadRequestResponse();
            }

            var user = await UserManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return CreateNotFoundResponse();
            }

            user.LockoutEnabled = false;
            await UserManager.UpdateAsync(user);
            return RedirectToAction(PetrovichRoutes.UserManagement.Active);
        }
    }
}