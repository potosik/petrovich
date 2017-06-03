using System;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class ViewContextExtensions
    {
        public static string GetAction(this ViewContext context)
        {
            return context.Controller.GetAction();
        }

        public static string GetController(this ViewContext context)
        {
            return context.Controller.GetController();
        }

        public static string GetId(this ViewContext context)
        {
            return context.Controller.GetId();
        }

        public static bool ActionEquals(this ViewContext context, string action)
        {
            return String.Equals(context.GetAction(), action, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}