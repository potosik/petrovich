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
        public async Task<ActionResult> GroupList()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GroupCreate()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GroupEdit()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GroupDelete()
        {
            return View();
        }
    }
}