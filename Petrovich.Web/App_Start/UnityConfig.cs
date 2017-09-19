using System;
using Microsoft.Practices.Unity;
using Petrovich.Core.Composition;
using Petrovich.Web.Composition;
using System.Linq;

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
            RegisterTypes(container, new CompositionModule());
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        
        private static void RegisterTypes(UnityContainer container, ICompositionModule compositionModule)
        {
            compositionModule.RegisterTypes(container);

            if (compositionModule.InnerModules != null && compositionModule.InnerModules.Any())
            {
                foreach (var module in compositionModule.InnerModules)
                {
                    RegisterTypes(container, module);
                }
            }
        }
    }
}
