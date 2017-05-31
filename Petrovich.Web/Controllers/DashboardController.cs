using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    [LoggableActions]
    public class DashboardController : BaseController
    {
        public DashboardController(ILoggingService logging)
            : base(logging)
        {
        }

        public async Task<ActionResult> Index()
        {
            await logger.LogNoneAsync("Lol!!!");
            var a= HttpContext.User.Identity.Name;

            return View();
        }
    }
}