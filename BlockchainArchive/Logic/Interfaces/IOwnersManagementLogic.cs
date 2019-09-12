using System.Collections.Generic;
using System.Threading.Tasks;
using BlockchainArchive.Models;

namespace BlockchainArchive.Logic
{
    public interface IOwnersManagementLogic
    {
        Task<List<Owner>> GetOwnersAsync();
        Task SaveOwner(Owner owner);
        Task<Owner> GetOwner(int id);
        Task RemoveOwner(int id);
    }
}