using Petrovich.Web.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public async Task<ActionResult> CategoryList()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CategoryCreate()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CategoryEdit()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CategoryDelete()
        {
            return View();
        }
    }
}