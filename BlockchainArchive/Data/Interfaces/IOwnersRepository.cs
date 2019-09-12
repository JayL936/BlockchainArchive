using System.Collections.Generic;
using System.Threading.Tasks;
using BlockchainArchive.Models;

namespace BlockchainArchive.Data
{
    public interface IOwnersRepository
    {
        Task<List<Owner>> GetAllOwners();
        Task SaveOwner(Owner owner);
        Task<Owner> GetOwner(int id);
        void RemoveOwner(Owner owner);
    }
}