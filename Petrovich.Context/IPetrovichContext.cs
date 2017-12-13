using Petrovich.Context.Entities;
using System.Data.Entity;

namespace Petrovich.Context
{
    public interface IPetrovichContext : IPetrovichContextBase
    {
        IDbSet<Log> Logs { get; set; }

        IDbSet<Branch> Branches { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Group> Groups { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<FullImage> FullImages { get; set; }

        IDbSet<Client> Clients { get; set; }
    }
}
