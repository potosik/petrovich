using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;
using Petrovich.Business.Data;
using Petrovich.Business.PerformanceCounters;
using Petrovich.Business.Services;

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
            container.RegisterType<IBranchService, BranchService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IGroupService, GroupService>();
            container.RegisterType<IProductService, ProductService>();

            container.RegisterType<ILogDataSource, LogPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(ILogDataSource), "LogDataSource")));
            container.RegisterType<IBranchDataSource, BranchPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(IBranchDataSource), "BranchDataSource")));
            container.RegisterType<ICategoryDataSource, CategoryPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(ICategoryDataSource), "CategoryDataSource")));
            container.RegisterType<IGroupDataSource, GroupPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(IGroupDataSource), "GroupDataSource")));
            container.RegisterType<IProductDataSource, ProductPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(IProductDataSource), "ProductDataSource")));
        }
    }
}
