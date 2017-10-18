using Petrovich.Business.Models.Base;
using Petrovich.Web.Properties;
using System;

namespace Petrovich.Web.Core.Extensions
{
    public static class IPriceableEntityExtensions
    {
        public static string GetPriceInformation(this IPriceableEntity entity)
        {
            if (!entity.Price.HasValue || !entity.PriceType.HasValue)
            {
                return Resources.Price_Format_NotAvailable;
            }

            var priceType = Resources.ResourceManager.GetString($"Price_Format_{entity.PriceType.Value}");
            var priceValue = entity.Price.Value.ToString("N2");
            var priceFormat = Resources.Price_Format;

            return String.Format(priceFormat, priceValue, priceType);
        }
    }
}