using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Auctioneers
    {
        [Key]
        public int AuctioneerId { get; set; }

       public ICollection<Users> Users { get; set; }
        public ICollection<Company> Company { get; set; }
        public ICollection<Products> Products { get; set; }

        //[ForeignKey("Users")]
        //public int UserId { get; set; }
        //public Users User { get; set; }

        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public int StaffNo { get; set; }

        //[ForeignKey("Company")]
        //public int CompanyId { get; set; }
        //public Company Company { get; set; }

        //[ForeignKey("Products")]
        //public int ProductId { get; set; }
        //public Products Product { get; set; }
    }
}
