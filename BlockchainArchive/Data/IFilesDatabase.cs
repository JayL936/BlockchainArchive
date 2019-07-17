using BlockchainArchive.Models;

namespace BlockchainArchive.Data
{
    public interface IFilesDatabase
    {
        void Save(File file);
    }
}