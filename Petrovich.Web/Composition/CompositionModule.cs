using Petrovich.Core.Composition;
using Microsoft.Practices.Unity;
using Unity;

namespace Petrovich.Web.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
            new Business.Composition.CompositionModule(),
            new Repositories.Composition.CompositionModule(),
            new DataSource.Composition.CompositionModule(),
        };

        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}