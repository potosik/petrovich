using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Context
{
    public class PetrovichContextFactory : IPetrovichContextFactory
    {
        public IPetrovichContext CreateContext()
        {
            return new PetrovichContext();
        }
    }
}
