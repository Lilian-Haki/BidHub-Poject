using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Bidders
    {
        [Key]
        public int BidderId {get; set; }
        public string CompanyUrl { get; set; }
        //Foreign Key
        public Guid UserId { get; set; }
        public Users User { get; set; }
    }
}
