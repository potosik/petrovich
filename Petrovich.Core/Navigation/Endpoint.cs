using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrovich.Core.Navigation
{
    public class Endpoint
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public Endpoint(string controller, string action)
        {
            Controller = controller;
            Action = action;
        }
    }
}
