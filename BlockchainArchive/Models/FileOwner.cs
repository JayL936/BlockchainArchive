using BlockchainArchive.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class FileOwner
    {
        [Key]
        public Guid FileGuid { get; set; }
        public File File { get; set; }

        [Key]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
