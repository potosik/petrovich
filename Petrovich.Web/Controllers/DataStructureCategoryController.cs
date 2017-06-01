using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Petrovich.Web.Controllers
{
    public partial class DataStructureController : BaseController
    {
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