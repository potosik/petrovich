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
    public partial class DataStructureController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> SubcategoryList()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SubcategoryCreate()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SubcategoryEdit()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SubcategoryDelete()
        {
            return View();
        }
    }
}