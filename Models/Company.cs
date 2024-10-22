using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; } 
        public string CompanyName { get; set; }
        public string Company_url { get; set; }

        public string Location { get; set; }
        public bool Status { get; set; }
        public DateTime DateAdded { get; set; }

        //public ICollection<Auctioneers> Auctioneers { get; set; }
    }
}
