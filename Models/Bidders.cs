using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Bidders
    {
        [Key]
        public int BidderId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users User { get; set; }
    }
}
