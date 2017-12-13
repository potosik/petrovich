using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;
using Petrovich.Business.Data;
using Petrovich.Business.PerformanceCounters;
using Petrovich.Business.Services;
using Petrovich.Business.Logging;
using Unity;
using Unity.Injection;

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
            container.RegisterType<IDataStructureService, DataStructureService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IFullImageService, FullImageService>();
            container.RegisterType<IClientService, ClientService>();

            container.RegisterType<ILogDataSource, LogPerformanceCounter>(new InjectionConstructor(new ResolvedParameter(typeof(ILogDataSource), "LogDataSource")));
            container.RegisterType<IBranchDataSource, BranchPerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(IBranchDataSource), "BranchDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
            container.RegisterType<ICategoryDataSource, CategoryPerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(ICategoryDataSource), "CategoryDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
            container.RegisterType<IGroupDataSource, GroupPerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(IGroupDataSource), "GroupDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
            container.RegisterType<IProductDataSource, ProductPerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(IProductDataSource), "ProductDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
            container.RegisterType<IFullImageDataSource, FullImagePerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(IFullImageDataSource), "FullImageDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
            container.RegisterType<IClientDataSource, ClientPerformanceCounter>(new InjectionConstructor(
                new ResolvedParameter(typeof(IClientDataSource), "ClientDataSource"),
                new ResolvedParameter(typeof(ILoggingService))));
        }
    }
}
