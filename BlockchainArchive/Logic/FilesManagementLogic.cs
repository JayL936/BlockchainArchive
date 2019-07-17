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
        private IFilesDatabase _filesDatabase;
        private IBlobStorage _blobStorage;

        public FilesManagementLogic(IFilesDatabase filesDatabase, IBlobStorage blobStorage)
        {
            _filesDatabase = filesDatabase;
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
                Name = uploadedFile.Name
            };

            _filesDatabase.Save(file);
        }
    }
}
