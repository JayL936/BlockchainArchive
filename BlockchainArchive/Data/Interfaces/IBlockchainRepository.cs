using System.Threading.Tasks;
using BlockchainArchive.Models;

namespace BlockchainArchive.Data.Interfaces
{
    public interface IBlockchainRepository
    {
        Task<StoredContract> GetStoredContract(string name);
        Task SaveStoredContract(StoredContract contract);
    }
}