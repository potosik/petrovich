using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Core.Security.Attributes;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize]
    public class DashboardController : BaseController
    {
        public DashboardController(ILoggingService logging)
            : base(logging)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}