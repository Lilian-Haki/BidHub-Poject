using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidHub_Poject.Models
{
    public class ProductPhotos
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoUrl { get; set; }

        // Foreign key
        //[ForeignKey("Products")]
        //public int ProductId { get; set; }
        //public Products Product { get; set; }

        //public ICollection<Products> Products { get; set; }
    }
}
