using BlockchainArchive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public class FilesDatabase : IFilesDatabase
    {
        private ApplicationDbContext _context;

        public FilesDatabase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void Save(File file)
        {
            await _context.AddAsync(file);
            await _context.SaveChangesAsync();
        }
    }
}
