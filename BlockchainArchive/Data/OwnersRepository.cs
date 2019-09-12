using BlockchainArchive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public class OwnersRepository : IOwnersRepository
    {
        private ApplicationDbContext _context;

        public OwnersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> GetAllOwners()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> GetOwner(int id)
        {
            return await _context.Owners.FirstOrDefaultAsync(o => o.Id == id);
        }

        public void RemoveOwner(Owner owner)
        {
            _context.Remove(owner);
        }

        public async Task SaveOwner(Owner owner)
        {
            await _context.AddAsync(owner);
            await _context.SaveChangesAsync();
        }
    }
}
