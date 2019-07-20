using BlockchainArchive.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public interface IFilesRepository
    {
        Task SaveAsync(File file);
        Task<IEnumerable<File>> GetFilesAsync();
        Task<File> GetFileAsync(Guid guid);
        void DeleteFile(File file);
        void UpdateFile(File file);
        Task<bool> FileExists(Guid guid);
    }
}