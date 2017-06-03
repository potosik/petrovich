using System.Threading.Tasks;
using Petrovich.Context.Entities;

namespace Petrovich.Repositories
{
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        Task<Branch> FindByInventoryPartAsync(string inventoryPart);
    }
}
