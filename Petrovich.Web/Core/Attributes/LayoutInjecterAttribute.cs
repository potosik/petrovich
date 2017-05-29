﻿using System.Web.Mvc;

namespace Petrovich.Web.Core.Attributes
{
    internal class LayoutInjecterAttribute : ActionFilterAttribute
    {
        private readonly string _masterName;

        public LayoutInjecterAttribute(string masterName)
        {
            _masterName = masterName;
        }
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = _masterName;
            }
        }
    }
}