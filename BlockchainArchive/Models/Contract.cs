using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class StoredContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public static class ContractHelpers
    {
        public static string GetDocumentFunctionName = "getDocumentHash";
        public static string SaveDocumentFunctionName = "saveDocumentHash";
        public static string ContractName = "GetAndSaveDocumentHash";
    }
}
