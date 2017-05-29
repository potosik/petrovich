using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetAction(this ControllerBase controller)
        {
            var rawValue = controller.ValueProvider.GetValue("action").RawValue;
            return ProcessRawValue(rawValue);
        }

        public static string GetController(this ControllerBase controller)
        {
            var rawValue = controller.ValueProvider.GetValue("controller").RawValue;
            return ProcessRawValue(rawValue);
        }

        public static string GetId(this ControllerBase controller)
        {
            var rawValue = controller.ValueProvider.GetValue("id").RawValue;
            return ProcessRawValue(rawValue);
        }

        private static string ProcessRawValue(object rawValue)
        {
            return rawValue?.ToString();
        }
    }
}