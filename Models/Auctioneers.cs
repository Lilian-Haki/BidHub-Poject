using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidHub_Poject.Models
{
    public class Auctioneers
    {
        [Key]
        public int AuctioneerId { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public int StaffNo { get; set; }
        
        //[ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        // Foreign key
        public int UserId { get; set; }
        public Users User { get; set; }
        public ICollection<Products> Products { get; set; }


    }
}
