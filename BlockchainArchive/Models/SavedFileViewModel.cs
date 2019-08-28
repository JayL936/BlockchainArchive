using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class SavedFileViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string StorageUrl { get; set; }
        public BlockchainHistory LastHistoryEntry { get; set; }
        public ICollection<Owner> Owners { get; set; }

        public SavedFileViewModel(File file)
        {
            Guid = file.Guid;
            Name = file.Name;
            StorageUrl = file.StorageUrl;
            LastHistoryEntry = file.HistoryEntries?.OrderByDescending(h => h.Timestamp).FirstOrDefault();
            Owners = file.FileOwners.Select(f => f.Owner).ToList();
        }
    }
}
