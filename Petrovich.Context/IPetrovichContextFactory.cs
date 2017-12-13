using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context
{
    public interface IPetrovichContextFactory
    {
        IPetrovichContext CreateContext();
    }
}
