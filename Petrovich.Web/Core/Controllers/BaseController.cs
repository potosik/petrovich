﻿using Petrovich.Core.Navigation;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        protected RedirectToRouteResult RedirectToAction(Endpoint endpoint)
        {
            return RedirectToAction(endpoint.Action, endpoint.Controller);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(PetrovichRoutes.Dashboard.Index);
        }
    }
}