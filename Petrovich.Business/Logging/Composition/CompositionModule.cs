using Microsoft.Practices.Unity;
using Petrovich.Core.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
