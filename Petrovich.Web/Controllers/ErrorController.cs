using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
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

        [LayoutInjecter("_LayoutEmpty")]
        public ActionResult BadRequest()
        {
            return View();
        }
    }
}