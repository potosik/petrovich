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
            logger.LogNone($"Page {actionName}.{controllerName} requested.");

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            var actionName = filterContext.Controller.GetAction();
            var controllerName = filterContext.Controller.GetController();

            if (!filterContext.Canceled)
            {
                logger.LogNone($"Page {actionName}.{controllerName} displayed.");
            }
            else
            {
                logger.LogNone($"Page {actionName}.{controllerName} canceled.");
            }
        }
    }
}