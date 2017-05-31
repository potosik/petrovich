﻿using Petrovich.Context.Entities;
using Petrovich.Context.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Petrovich.Context
{
    public interface IPetrovichContext : IPetrovichContextBase
    {
        IDbSet<Log> Logs { get; set; }
    }
}
