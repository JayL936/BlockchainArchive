using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class FileViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string StorageUrl { get; set; }
        public BlockchainHistory LastHistoryEntry { get; set; }

        public FileViewModel(File file)
        {
            Guid = file.Guid;
            Name = file.Name;
            StorageUrl = file.StorageUrl;
            LastHistoryEntry = file.HistoryEntries?.OrderByDescending(h => h.Timestamp).FirstOrDefault();
        }
    }
}
