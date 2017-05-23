using Petrovich.Core;
using Petrovich.Web.Security.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Petrovich.Web.Startup;

namespace Petrovich.Web.Controllers
{
    [ClaimsAuthorize(Claims = new [] { PetrovichClaims.UserManagement })]
    public class ManageUsersController : Controller
    {
        // GET: ManageUsers
        public ActionResult Index()
        {
            return View();
        }
    }
}