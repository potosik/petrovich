using Petrovich.Web.Core.Controllers;
using System.Web.Mvc;
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