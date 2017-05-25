using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Petrovich.Core.Composition;
using Petrovich.Web.Composition;

namespace Petrovich.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            RegisterTypes(container, new CompositionModule());
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<IProductRepository, ProductRepository>();
        }

        private static void RegisterTypes(UnityContainer container, CompositionModule compositionModule)
        {
            compositionModule.RegisterTypes(container);

            if (compositionModule.InnerModules != null && compositionModule.InnerModules.Length > 0)
            {
                foreach (var module in compositionModule.InnerModules)
                {
                    RegisterTypes(container, compositionModule);
                }
            }
        }
    }
}
