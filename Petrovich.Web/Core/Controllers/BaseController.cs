using Petrovich.Business.Logging;
using Petrovich.Core.Navigation;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILoggingService logger;

        public BaseController(ILoggingService logger)
        {
            this.logger = logger;
        }

        protected RedirectToRouteResult RedirectToAction(Endpoint endpoint)
        {
            return RedirectToAction(endpoint.Action, endpoint.Controller);
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(PetrovichRoutes.Dashboard.Index);
        }

        protected ActionResult CreateNotFoundResponse()
        {
            return RedirectToAction(PetrovichRoutes.Error.NotFound);
        }

        protected async Task<ActionResult> CreateNotFoundResponseAsync(Exception ex)
        {
            await logger.LogErrorAsync(ex);
            return CreateNotFoundResponse();
        }

        protected ActionResult CreateBadRequestResponse()
        {
            return RedirectToAction(PetrovichRoutes.Error.BadRequest);
        }

        protected async Task<ActionResult> CreateBadRequestResponseAsync(Exception ex)
        {
            await logger.LogErrorAsync(ex);
            return CreateBadRequestResponse();
        }

        protected ActionResult CreateInternalServerErrorResponse()
        {
            return RedirectToAction(PetrovichRoutes.Error.Index);
        }

        protected async Task<ActionResult> CreateInternalServerErrorResponseAsync(Exception ex)
        {
            await logger.LogCriticalAsync(ex);
            return CreateInternalServerErrorResponse();
        }
    }
}