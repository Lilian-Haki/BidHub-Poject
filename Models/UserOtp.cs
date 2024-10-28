using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class UserOtp
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
