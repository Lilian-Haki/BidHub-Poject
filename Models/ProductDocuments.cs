using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidHub_Poject.Models
{
    public class ProductDocuments
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentUrl { get; set; }


       //Foreign Key
        public int ProductId { get; set; }
        public Products Product { get; set; }

    }
}
