using Petrovich.Business.Data;
using Petrovich.Core.Composition;
using Petrovich.DataSource.Mappers;
using Petrovich.DataSource.Mappers.Concrete;
using Unity;

namespace Petrovich.DataSource.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
        };

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IClientDataSource, ClientDataSource>("ClientDataSource");

            container.RegisterType<IClientMapper, ClientMapper>();
        }
    }
}
