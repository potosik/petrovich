using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Composition
{
    public interface ICompositionModule
    {
        ICompositionModule[] InnerModules { get; }
        void RegisterTypes(IUnityContainer container);
    }
}
