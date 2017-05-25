using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrovich.Context.Entities;
using Petrovich.Context.Migrations;

namespace Petrovich.Context
{
    public class PetrovichContext : DbContext, IPetrovichContext
    {
        public PetrovichContext()
            : base("databaseConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetrovichContext, Configuration>());
        }

        public IDbSet<Log> Logs { get; set; }
    }
}
