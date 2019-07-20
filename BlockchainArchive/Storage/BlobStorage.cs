using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Storage
{
    public class BlobStorage : IBlobStorage
    {
        private CloudStorageAccount _cloudStorageAccount;
        private CloudBlobClient _cloudBlobClient;
        private CloudBlobContainer _cloudBlobContainer;

        public BlobStorage(string connectionString)
        {
            if (CloudStorageAccount.TryParse(connectionString, out _cloudStorageAccount))
            {
                _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
                _cloudBlobContainer = _cloudBlobClient.GetContainerReference("archivebc");

                _cloudBlobContainer.CreateIfNotExists();

                var permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                _cloudBlobContainer.SetPermissions(permissions);
            }
            else
            {
                throw new Exception("Blob storage connection not valid!");
            }
        }

        public async Task<Uri> UploadFile(Stream stream, string fileName)
        {
            var blob = _cloudBlobContainer.GetBlockBlobReference(fileName);
            await blob.UploadFromStreamAsync(stream);

            return blob.Uri;
        }

        public void DeleteFile(string fileName)
        {
            var blob = _cloudBlobContainer.GetBlockBlobReference(fileName);
            if (blob != null)
                blob.Delete();
        }
    }
}
