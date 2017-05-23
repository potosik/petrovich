using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}