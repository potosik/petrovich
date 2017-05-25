using Petrovich.Core.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Petrovich.Business.Data;
using Petrovich.Business.PerformanceCounters;

namespace Petrovich.Business.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
            new Logging.Composition.CompositionModule(),
        };

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogDataSource, LogPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(ILogDataSource), "LogDataSource")));
        }
    }
}
