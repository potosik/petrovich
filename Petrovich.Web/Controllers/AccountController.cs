using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Petrovich.Web.Models;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Core.Navigation;
using Petrovich.Web.Models.Account;
using Petrovich.Business.Logging;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    [LoggableActions]
    public class AccountController : IdentityController
    {
        public AccountController(ILoggingService logging)
            : base(logging)
        {
        }

        [AllowAnonymous]
        [LayoutInjecter("_LayoutEmptyWhite")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [LayoutInjecter("_LayoutEmptyWhite")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                await logger.LogInvalidModelAsync(model.GetType());
                return View(model);
            }

            var status = SignInStatus.Success;
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null && user.LockoutEnabled)
            {
                status = SignInStatus.Failure;
                await logger.LogInformationAsync($"Login failed for {model.Email}. Trying to sign in with deleted user account.");
            }
            else
            {
                status = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                await logger.LogNoneAsync($"Login process completed.");
            }

            switch (status)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    await logger.LogInformationAsync($"Login failed for {model.Email}.");
                    ModelState.AddModelError("", "Введены неверные логин или пароль.");
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            await logger.LogNoneAsync($"Logout process completed.");
            return RedirectToAction(PetrovichRoutes.Dashboard.Index);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await logger.LogInvalidModelAsync(model.GetType());
                return View(model);
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return RedirectToAction(PetrovichRoutes.Account.ChangePasswordSuccess.Action);
            }

            await logger.LogErrorAsync($"Unsuccessful password change for {User.Identity.GetUserId()}.");
            
            AddErrors(result);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}