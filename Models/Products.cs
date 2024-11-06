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
        public double ReservePrice { get; set; }
        public string Location { get; set; }

        public int AuctioneerId { get; set; }
        public Auctioneers Auctioneers { get; set; }
       
        public ICollection<ProductPhotos> Photos { get; set; }
        public ICollection<ProductDocuments> Documents { get; set; }
        public ICollection<BViewing> BViewings { get; set; }
        public ICollection<BidDates> BidDates { get; set; }



    }
}
