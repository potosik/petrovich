using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [Authorize]
    public class ErrorController : BaseController
    {
        public ErrorController(ILoggingService logging)
            : base(logging)
        {
        }

        [LayoutInjecter("_LayoutEmpty")]
        public async Task<ActionResult> Index()
        {
            await logger.LogCriticalAsync("500 Internal server error page displayed");
            return View();
        }

        [LayoutInjecter("_LayoutEmpty")]
        public async Task<ActionResult> NotFound()
        {
            await logger.LogErrorAsync("404 Not found page displayed");
            return View();
        }

        [LayoutInjecter("_LayoutEmpty")]
        public async Task<ActionResult> BadRequest()
        {
            await logger.LogErrorAsync("400 Bad request error page displayed");
            return View();
        }
    }
}