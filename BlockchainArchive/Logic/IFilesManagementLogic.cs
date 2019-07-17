using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlockchainArchive.Logic
{
    public interface IFilesManagementLogic
    {
        Task SaveUploadedFile(IFormFile file);
    }
}