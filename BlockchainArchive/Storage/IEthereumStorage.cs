using System;
using System.Threading.Tasks;

namespace BlockchainArchive.Storage
{
    public interface IEthereumStorage
    {
        Task<bool> SendDocumentHashToChain(string hash, string guid);
        Task<string> GetDocumentHashFromChain(string guid);
    }
}