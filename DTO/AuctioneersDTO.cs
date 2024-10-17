using BidHub_Poject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidHub_Poject.DTO
{
    public class AuctioneersDTO
    {
        public string PhotoUrl { get; set; }
        public string Role { get; set; }
        public int StaffNo { get; set; }
    }
}
