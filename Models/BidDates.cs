using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class BidDates
    {
        [Key]
        public int BidDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
