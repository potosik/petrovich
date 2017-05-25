using Petrovich.Core.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Petrovich.Business.Data;
using Petrovich.Repositories.DataSources;
using Petrovich.Repositories.Concrete;

namespace Petrovich.Repositories.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
            new Mappers.Composition.CompositionModule(),

        };

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogDataSource, LogDataSource>("LogDataSource");

            container.RegisterType<ILogRepository, LogRepository>();
        }
    }
}
