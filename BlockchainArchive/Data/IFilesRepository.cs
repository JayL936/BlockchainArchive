using BlockchainArchive.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockchainArchive.Data
{
    public interface IFilesRepository
    {
        Task SaveAsync(File file);
        Task<IEnumerable<File>> GetFilesAsync();
    }
}