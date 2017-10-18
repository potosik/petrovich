using Petrovich.Business.Models;
using Petrovich.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.Extensions
{
    public static class ProductExtensions
    {
        public static string GetHierarchicalPrice(this Product product)
        {
            var result = product.GetPriceInformation();
            if (result != Resources.Price_Format_NotAvailable)
            {
                return result;
            }

            if (product.Group != null)
            {
                result = product.Group.GetPriceInformation();
                if (result != Resources.Price_Format_NotAvailable)
                {
                    return result;
                }
            }

            if (product.Category != null)
            {
                return product.Category.GetPriceInformation();
            }

            return result;
        }
    }
}