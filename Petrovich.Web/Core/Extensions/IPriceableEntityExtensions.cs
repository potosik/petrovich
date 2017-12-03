using Petrovich.Business.Models.Base;
using Petrovich.Business.Models.Enumerations;
using Petrovich.Core;
using Petrovich.Web.Properties;
using System;

namespace Petrovich.Web.Core.Extensions
{
    public static class IPriceableEntityExtensions
    {
        public static string GetPriceInformation(this IPriceableEntityModel entity, PriceCalculationTypeBusiness priceCalculationType)
        {
            if (!entity.Price.HasValue)
            {
                return Resources.Price_Format_NotAvailable;
            }

            var priceCalculationTypeString = Resources.ResourceManager.GetString($"PriceCalculationType_Abr_{priceCalculationType.ToString()}");
            var priceValue = entity.Price.Value.ToString(Constants.PriceValueStringFormat);
            var priceFormat = Resources.Price_Format;

            return String.Format(priceFormat, priceValue, priceCalculationTypeString);
        }
    }
}