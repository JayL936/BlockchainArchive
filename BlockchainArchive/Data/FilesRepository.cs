using BlockchainArchive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public class FilesRepository : IFilesRepository
    {
        private ApplicationDbContext _context;

        public FilesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(File file)
        {
            await _context.AddAsync(file);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<File>> GetFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }
    }
}
