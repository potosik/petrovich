using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;

namespace Petrovich.Context.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => null;

        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPetrovichContext, PetrovichContext>();
        }
    }
}
