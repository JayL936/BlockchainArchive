using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlockchainArchive.Models
{
    public class UploadFileViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Author { get; set; }
        public string Purpose { get; set; }
        public List<int> OwnersIds { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
