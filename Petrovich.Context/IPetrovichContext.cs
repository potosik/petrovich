using Petrovich.Context.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context
{
    public interface IPetrovichContext : IDisposable
    {
        IDbSet<Log> Logs { get; set; }
    }
}
