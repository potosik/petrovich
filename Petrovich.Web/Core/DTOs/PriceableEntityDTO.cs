using Petrovich.Business.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Core.DTOs
{
    public class PriceableEntityDTO
    {
        public double? Price { get; set; }
        public PriceTypeBusiness? PriceType { get; set; }
    }
}