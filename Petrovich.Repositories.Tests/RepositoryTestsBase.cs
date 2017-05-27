using Moq;
using Petrovich.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Repositories.Tests
{
    public class RepositoryTestsBase
    {
        protected static Mock<IPetrovichContext> CreateContext()
        {
            return new Mock<IPetrovichContext>();
        }
    }
}
