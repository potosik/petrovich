using Petrovich.Business.Logging;
using Petrovich.Core;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.PowerAdmin })]
    public class LoggingController : BaseController
    {
        public LoggingController(ILoggingService logging)
            : base(logging)
        {
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }
    }
}