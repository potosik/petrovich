using Microsoft.Practices.Unity;
using Petrovich.Business.Logging;
using Petrovich.Web.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Attributes
{
    internal class LoggableActionsAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILoggingService logger { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var userName = filterContext.HttpContext?.User?.Identity?.Name;

            logger.LogNone($"Page {actionName}.{controllerName} requested by {userName}.");

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            var actionName = filterContext.Controller.GetAction();
            var controllerName = filterContext.Controller.GetController();
            var userName = filterContext.HttpContext?.User?.Identity?.Name;

            if (!filterContext.Canceled)
            {
                logger.LogNone($"Page {actionName}.{controllerName} displayed for {userName}.");
            }
            else
            {
                logger.LogNone($"Page {actionName}.{controllerName} canceled for {userName}.");
            }
        }
    }
}