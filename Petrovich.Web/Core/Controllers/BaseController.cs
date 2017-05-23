using Petrovich.Core.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        public RedirectToRouteResult RedirectToAction(Endpoint endpoint)
        {
            return RedirectToAction(endpoint.Action, endpoint.Controller);
        }
    }
}