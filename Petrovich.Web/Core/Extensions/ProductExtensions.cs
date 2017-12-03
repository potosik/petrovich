using Petrovich.Business.Models;
using Petrovich.Web.Core.DTOs;
using Petrovich.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.Extensions
{
    public static class ProductExtensions
    {
        public static string GetHierarchicalPrice(this ProductModel product)
        {
            var result = product.GetPriceInformation(product.Category.PriceCalculationType);
            if (result != Resources.Price_Format_NotAvailable)
            {
                return result;
            }

            if (product.Group != null)
            {
                result = product.Group.GetPriceInformation(product.Category.PriceCalculationType);
                if (result != Resources.Price_Format_NotAvailable)
                {
                    return result;
                }
            }

            if (product.Category != null)
            {
                return product.Category.GetPriceInformation(product.Category.PriceCalculationType);
            }

            return result;
        }

        public static PriceableEntityDTO GetHierarchicalPriceDTO(this ProductModel product)
        {
            if (product.Price.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Price,
                    PriceCalculationType = product.Category.PriceCalculationType,
                };
            }


            if (product.Group != null && product.Group.Price.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Group.Price,
                    PriceCalculationType = product.Category.PriceCalculationType,
                };
            }


            if (product.Category.Price.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Category.Price,
                    PriceCalculationType = product.Category.PriceCalculationType,
                };
            }

            return new PriceableEntityDTO();
        }
    }
}