using Petrovich.Core.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace Petrovich.Web.Composition
{
    public class CompositionModule : ICompositionModule
    {
        public ICompositionModule[] InnerModules => new ICompositionModule[] 
        {
            new Business.Composition.CompositionModule(),
            new Repositories.Composition.CompositionModule(),
        };

        public void RegisterTypes(IUnityContainer container)
        {
        }
    }
}