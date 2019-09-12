using BlockchainArchive.Data;
using BlockchainArchive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Logic
{
    public class OwnersManagementLogic : IOwnersManagementLogic
    {
        public IOwnersRepository _ownersRepository;

        public OwnersManagementLogic(IOwnersRepository ownersRepository)
        {
            _ownersRepository = ownersRepository;
        }

        public async Task<Owner> GetOwner(int id)
        {
            return await _ownersRepository.GetOwner(id);
        }

        public async Task<List<Owner>> GetOwnersAsync()
        {
            return await _ownersRepository.GetAllOwners();
        }

        public async Task RemoveOwner(int id)
        {
            var owner = await GetOwner(id);

            if (owner != null)
                _ownersRepository.RemoveOwner(owner);
        }

        public async Task SaveOwner(Owner owner)
        {
            await _ownersRepository.SaveOwner(owner);
        }
    }
}
