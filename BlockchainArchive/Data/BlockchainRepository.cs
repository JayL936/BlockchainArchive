using BlockchainArchive.Data.Interfaces;
using BlockchainArchive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public class BlockchainRepository : IBlockchainRepository
    {
        private ApplicationDbContext _context;

        public BlockchainRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StoredContract> GetStoredContract(string name)
        {
            return await _context.StoredContracts.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task SaveStoredContract(StoredContract contract)
        {
            await _context.AddAsync(contract);
            await _context.SaveChangesAsync();
        }
    }
}
