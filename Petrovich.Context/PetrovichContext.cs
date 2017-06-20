using System.Data.Entity;
using Petrovich.Context.Entities;
using Petrovich.Context.Migrations;

namespace Petrovich.Context
{
    public class PetrovichContext : PetrovichContextBase, IPetrovichContext
    {
        public PetrovichContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetrovichContext, Configuration>());
        }

        public IDbSet<Log> Logs { get; set; }

        public IDbSet<Branch> Branches { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<FullImage> FullImages { get; set; }
    }
}
