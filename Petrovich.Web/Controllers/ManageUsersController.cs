using Petrovich.Core;
using Petrovich.Web.Core.Controllers;
using Petrovich.Web.Security.Attributes;
using System.Web.Mvc;

namespace Petrovich.Web.Controllers
{
    [ClaimsAuthorize(Claims = new [] { PetrovichClaims.UserManagement })]
    public class ManageUsersController : BaseController
    {
        // GET: ManageUsers
        public ActionResult Index()
        {
            return View();
        }
    }
}