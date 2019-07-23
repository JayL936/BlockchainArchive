using BlockchainArchive.Data;
using BlockchainArchive.Models;
using BlockchainArchive.Models.Enums;
using BlockchainArchive.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlockchainArchive.Logic
{
    public class FilesManagementLogic : IFilesManagementLogic
    {
        private IFilesRepository _filesRepository;
        private IBlobStorage _blobStorage;
        private IEthereumStorage _ethereumStorage;

        public FilesManagementLogic(IFilesRepository filesRepository, IBlobStorage blobStorage, IEthereumStorage ethereumStorage)
        {
            _filesRepository = filesRepository;
            _blobStorage = blobStorage;
            _ethereumStorage = ethereumStorage;
        }

        public async Task<bool> SaveUploadedFile(IFormFile uploadedFile)
        {
            var stream = uploadedFile.OpenReadStream();
            var storageUri = await _blobStorage.UploadFile(stream, uploadedFile.FileName);

            var file = new File
            {
                StorageUrl = storageUri.AbsolutePath,
                Guid = Guid.NewGuid(),
                Name = uploadedFile.FileName,
                HistoryEntries = new List<BlockchainHistory>()                
            };

            using (var md5 = MD5.Create())
            {
                var isSuccess = await _ethereumStorage.SendDocumentHashToChain(Convert.ToBase64String(md5.ComputeHash(stream)), file.Guid.ToString());
                if (!isSuccess)
                    return false;
            }

            file.HistoryEntries.Add(new BlockchainHistory
            {
                Timestamp = DateTime.Now,
                Status = BlockchainStatuses.Verified
            });

            await _filesRepository.SaveAsync(file);
            return true;
        }

        public async Task<IEnumerable<File>> GetFilesWithHistoryAsync()
        {
            return await _filesRepository.GetFilesWithHistoryAsync();
        }

        public async Task<File> GetFileWithHistoryAsync(Guid guid)
        {
            return await _filesRepository.GetFileWithHistoryAsync(guid);
        }

        public async Task DeleteFileAsync(Guid guid)
        {
            var file = await _filesRepository.GetFileAsync(guid);
            if (file != null)
            {
                _blobStorage.DeleteFile(file.Name);
                _filesRepository.DeleteFile(file);
            }
        }

        public async Task<int> UpdateFileAsync(File file)
        {
            try
            {
                _filesRepository.UpdateFile(file);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _filesRepository.FileExists(file.Guid))
                {
                    return StatusCodes.Status404NotFound;
                }
                else
                {
                    throw;
                }
            }

            return StatusCodes.Status200OK;
        }
    }
}
