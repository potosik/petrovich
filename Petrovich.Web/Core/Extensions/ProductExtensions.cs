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

        public static PriceableEntityDTO GetHierarchicalPriceDTO(this ProductModel product)
        {
            if (product.Price.HasValue && product.PriceType.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Price,
                    PriceType = product.PriceType,
                };
            }


            if (product.Group != null && product.Group.Price.HasValue && product.Group.PriceType.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Group.Price,
                    PriceType = product.Group.PriceType,
                };
            }


            if (product.Category.Price.HasValue && product.Category.PriceType.HasValue)
            {
                return new PriceableEntityDTO()
                {
                    Price = product.Category.Price,
                    PriceType = product.Category.PriceType,
                };
            }

            return new PriceableEntityDTO();
        }
    }
}