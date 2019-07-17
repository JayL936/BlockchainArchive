﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace BlockchainArchive.Storage
{
    public interface IBlobStorage
    {
        Task<Uri> UploadFile(Stream stream, string fileName);
    }
}