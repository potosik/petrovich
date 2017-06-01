using Moq;
using Petrovich.Context;

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
