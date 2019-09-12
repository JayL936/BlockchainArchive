using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models.Enums
{
    [Flags]
    public enum OwnerType
    {
        Author = 0,
        Owner = 1,
        Publisher = 2
    }
}
