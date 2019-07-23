using BlockchainArchive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class BlockchainHistory
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public BlockchainStatuses Status { get; set; }

        public Guid FileGuid { get; set; }
        public File File { get; set; }
    }
}
