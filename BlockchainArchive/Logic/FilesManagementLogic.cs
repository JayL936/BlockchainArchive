using BlockchainArchive.Data;
using BlockchainArchive.Models;
using BlockchainArchive.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Logic
{
    public class FilesManagementLogic : IFilesManagementLogic
    {
        private IFilesRepository _filesRepository;
        private IBlobStorage _blobStorage;

        public FilesManagementLogic(IFilesRepository filesRepository, IBlobStorage blobStorage)
        {
            _filesRepository = filesRepository;
            _blobStorage = blobStorage;
        }

        public async Task SaveUploadedFile(IFormFile uploadedFile)
        {
            var stream = uploadedFile.OpenReadStream();
            var storageUri = await _blobStorage.UploadFile(stream, uploadedFile.FileName);

            var file = new File
            {
                StorageUrl = storageUri.AbsolutePath,
                Guid = Guid.NewGuid(),
                Name = uploadedFile.FileName
            };

            await _filesRepository.SaveAsync(file);
        }

        public async Task<IEnumerable<File>> GetFilesAsync()
        {
            return await _filesRepository.GetFilesAsync();
        }
    }
}
