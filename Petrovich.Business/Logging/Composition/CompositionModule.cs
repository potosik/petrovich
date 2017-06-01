using Microsoft.Practices.Unity;
using Petrovich.Core.Composition;

namespace Petrovich.Business.Logging.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => null;

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILoggingService, LoggingService>();
        }
    }
}
