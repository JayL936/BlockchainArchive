﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlockchainArchive.Models;
using Microsoft.AspNetCore.Http;

namespace BlockchainArchive.Logic
{
    public interface IFilesManagementLogic
    {
        Task<bool> SaveUploadedFile(UploadFileViewModel file);
        Task<bool> VerifyUploadedFile(Guid guid);
        Task<IEnumerable<File>> GetFilesWithHistoryAsync();
        Task<File> GetFileWithHistoryAsync(Guid guid);
        Task DeleteFileAsync(Guid guid);
        Task<int> UpdateFileAsync(File file);
    }
}