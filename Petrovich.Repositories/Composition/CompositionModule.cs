using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;
using Petrovich.Business.Data;
using Petrovich.Repositories.DataSources;
using Petrovich.Repositories.Concrete;
using Unity;

namespace Petrovich.Repositories.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
            new Context.Composition.CompositionModule(),
            new Mappers.Composition.CompositionModule(),

        };

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ILogDataSource, LogDataSource>("LogDataSource");
            container.RegisterType<IBranchDataSource, BranchDataSource>("BranchDataSource");
            container.RegisterType<ICategoryDataSource, CategoryDataSource>("CategoryDataSource");
            container.RegisterType<IGroupDataSource, GroupDataSource>("GroupDataSource");
            container.RegisterType<IProductDataSource, ProductDataSource>("ProductDataSource");
            container.RegisterType<IFullImageDataSource, FullImageDataSource>("FullImageDataSource");

            container.RegisterType<ILogRepository, LogRepository>();
            container.RegisterType<IBranchRepository, BranchRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IGroupRepository, GroupRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IFullImageRepository, FullImageRepository>();
        }
    }
}
