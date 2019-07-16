using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class File
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string StorageUrl { get; set; }
        public string BlockReference { get; set; }
    }
}
