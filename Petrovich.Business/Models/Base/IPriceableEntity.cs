﻿using Petrovich.Business.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Business.Models.Base
{
    public interface IPriceableEntity
    {
        double? Price { get; set; }
        PriceType? PriceType { get; set; }
    }
}
