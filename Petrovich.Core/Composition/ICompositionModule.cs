using Microsoft.Practices.Unity;

namespace Petrovich.Core.Composition
{
    public interface ICompositionModule
    {
        ICompositionModule[] InnerModules { get; }
        void RegisterTypes(IUnityContainer container);
    }
}
