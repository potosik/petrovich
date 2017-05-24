using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class ViewContextExtensions
    {
        public static string GetAction(this ViewContext context)
        {
            var rawValue = context.Controller.ValueProvider.GetValue("action").RawValue;
            return ProcessRawValue(rawValue);
        }

        public static string GetController(this ViewContext context)
        {
            var rawValue = context.Controller.ValueProvider.GetValue("controller").RawValue;
            return ProcessRawValue(rawValue);
        }

        public static string GetId(this ViewContext context)
        {
            var rawValue = context.Controller.ValueProvider.GetValue("id").RawValue;
            return ProcessRawValue(rawValue);
        }

        private static string ProcessRawValue(object rawValue)
        {
            return rawValue == null ? null : rawValue.ToString();
        }

        public static bool ActionEquals(this ViewContext context, string action)
        {
            return String.Equals(context.GetAction(), action, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}