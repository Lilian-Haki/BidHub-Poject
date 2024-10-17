using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BidHub_Poject.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ReasonForAuction { get; set; }
        public string OwnersName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public decimal ReservePrice { get; set; }
        public string Location { get; set; }


        //[ForeignKey("ProductPhotos")]
        //public int PhotoId { get; set; }
        //public ProductPhotos ProductPhoto { get; set; }

        //[ForeignKey("ProductDocuments")]
        //public int DocumentId { get; set; }
        //public ProductDocuments? ProductDocument { get; set; }

        //public ICollection<ProductPhotos> Photos { get; set; }
        //public ICollection<ProductDocuments>? Documents { get; set; } 
        public ICollection<Auctioneers> Auctioneers { get; set; }
        public ICollection<BViewing> BViewings { get; set; }
        public ICollection<BidDates> BidDates { get; set; }


    }
}
