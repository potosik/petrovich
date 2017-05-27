using Petrovich.Business.Logging;
using Petrovich.Web.Core.Controllers;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        protected readonly ILoggingService logger;

        public DashboardController(ILoggingService logger)
        {
            this.logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}