using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context.Entities;
using Petrovich.Context.Migrations;
using Petrovich.Context.Entities.Base;
using System.Web;
using System.Threading;

namespace Petrovich.Context
{
    public class PetrovichContext : PetrovichContextBase, IPetrovichContext
    {
        public PetrovichContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetrovichContext, Configuration>());
        }

        public IDbSet<Log> Logs { get; set; }
    }
}
