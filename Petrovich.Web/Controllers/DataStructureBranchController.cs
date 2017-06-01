using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using Petrovich.Business.Logging;
using Petrovich.Web.Core.Attributes;
using Petrovich.Web.Core.Security.Attributes;
using Petrovich.Core;
using System.Threading.Tasks;

namespace Petrovich.Web.Controllers
{
    [LoggableActions]
    [ClaimsAuthorize(Claims = new[] { PermissionClaims.ProductsAdmin })]
    public partial class DataStructureController : BaseController
    {
        public DataStructureController(ILoggingService logger)
            : base(logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult> BranchList()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BranchCreate()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BranchEdit()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BranchDelete()
        {
            return View();
        }
    }
}