using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class BViewing
    {
        [Key]
        public int ViewingId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products Product { get; set; }

        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}
