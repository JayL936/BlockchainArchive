using System.Collections.Generic;
using System.Threading.Tasks;
using BlockchainArchive.Models;
using Microsoft.AspNetCore.Http;

namespace BlockchainArchive.Logic
{
    public interface IFilesManagementLogic
    {
        Task SaveUploadedFile(IFormFile file);
        Task<IEnumerable<File>> GetFilesAsync();
    }
}