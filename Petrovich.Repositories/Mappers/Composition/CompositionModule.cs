using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;
using Petrovich.Repositories.Mappers.Concrete;
using Unity;

namespace Petrovich.Repositories.Mappers.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => null;

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogMapper, LogMapper>();
            container.RegisterType<IBranchMapper, BranchMapper>();
            container.RegisterType<ICategoryMapper, CategoryMapper>();
            container.RegisterType<IGroupMapper, GroupMapper>();
            container.RegisterType<IProductMapper, ProductMapper>();
            container.RegisterType<IFullImageMapper, FullImageMapper>();
        }
    }
}
