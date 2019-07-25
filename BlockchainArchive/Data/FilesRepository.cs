using BlockchainArchive.Data.Interfaces;
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(File file)
        {
            await _context.AddAsync(file);
            await SaveChangesAsync();
        }

        public async Task SaveAsync(BlockchainHistory history)
        {
            await _context.AddAsync(history);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<File>> GetFilesWithHistoryAsync()
        {
            return await _context.Files
                .Include(f => f.HistoryEntries)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<File> GetFileWithHistoryAsync(Guid guid)
        {
            return await _context.Files
                .Include(f => f.HistoryEntries)
                .FirstOrDefaultAsync(m => m.Guid == guid);
        }

        public async Task<File> GetFileAsync(Guid guid)
        {
            return await _context.Files.FirstOrDefaultAsync(m => m.Guid == guid);
        }

        public void DeleteFile(File file)
        {
            _context.Remove(file);
            _context.SaveChanges();
        }

        public void UpdateFile(File file)
        {
            _context.Update(file);
            _context.SaveChanges();
        }

        public async Task<bool> FileExists(Guid guid)
        {
            return await _context.Files.AnyAsync(f => f.Guid == guid);
        }
    }
}
