﻿using Petrovich.Business.Logging;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Core.Navigation;
using Petrovich.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Controllers
{
    public class BaseController : Controller
    {
        protected const int DefaultPageSize = 20;

        protected readonly ILoggingService logger;

        public BaseController(ILoggingService logger)
        {
            this.logger = logger;
        }

        protected RedirectToRouteResult RedirectToAction(Endpoint endpoint)
        {
            return RedirectToAction(endpoint.Action, endpoint.Controller);
        }

        protected RedirectToRouteResult RedirectToAction(Endpoint endpoint, object routeValues)
        {
            return RedirectToAction(endpoint.Action, endpoint.Controller, routeValues);
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
            return RedirectToAction(PetrovichRoutes.Error.NotFound, new { timeUtc = DateTime.UtcNow });
        }

        protected async Task<ActionResult> CreateNotFoundResponseAsync(Exception ex)
        {
            await logger.LogErrorAsync(ex);
            return CreateNotFoundResponse();
        }

        protected ActionResult CreateBadRequestResponse()
        {
            return RedirectToAction(PetrovichRoutes.Error.BadRequest, new { timeUtc = DateTime.UtcNow });
        }

        protected async Task<ActionResult> CreateBadRequestResponseAsync(Exception ex)
        {
            await logger.LogErrorAsync(ex);
            return CreateBadRequestResponse();
        }

        protected ActionResult CreateInternalServerErrorResponse()
        {
            return RedirectToAction(PetrovichRoutes.Error.Index, new { timeUtc = DateTime.UtcNow });
        }

        protected async Task<ActionResult> CreateInternalServerErrorResponseAsync(Exception ex)
        {
            await logger.LogCriticalAsync(ex);
            return CreateInternalServerErrorResponse();
        }

        protected JsonResult JsonAllowGet(JsonResponse response)
        {
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected List<SelectListItem> CreatePriceTypeSelectList()
        {
            var values = Enum.GetValues(typeof(PriceType));
            var list = new List<SelectListItem>() { new SelectListItem() };

            foreach (PriceType value in values)
            {
                var iValue = (int)value;
                var text = Properties.Resources.ResourceManager.GetString($"PriceType_{value}");
                list.Add(new SelectListItem() { Text = text, Value = iValue.ToString() });
            }

            return list;
        }
    }
}