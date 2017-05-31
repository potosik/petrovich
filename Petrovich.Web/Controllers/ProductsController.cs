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
    public class ProductsController : BaseController
    {
        public ProductsController(ILoggingService logger) 
            : base(logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete()
        {
            return View();
        }
    }
}