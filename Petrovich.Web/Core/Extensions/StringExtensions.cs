using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Petrovich.Web.Core.Extensions
{
    public static class StringExtensions
    {
        private const int DescriptionMaxLength = 150;

        public static string RenderImageForSmartCart(this string value, UrlHelper helper)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return helper.Content("~/Content/img/no-image.png");
            }

            return $"data:image/png;base64,{value}";
        }

        public static string CutDescription(this string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Length <= DescriptionMaxLength ? value : value.Substring(0, DescriptionMaxLength) + "...";
        }
    }
}