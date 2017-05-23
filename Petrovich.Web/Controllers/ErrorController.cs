using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    public class ErrorController : BaseController
    {
        [LayoutInjecter("_LayoutEmpty")]
        public ActionResult Index()
        {
            return View();
        }

        [LayoutInjecter("_LayoutEmpty")]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}