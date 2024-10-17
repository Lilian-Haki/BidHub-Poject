using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhysicalAddress { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Bidders> Bidders { get; set; }
        public ICollection<BViewing> BViewings { get; set; }
        public ICollection<Auctioneers> Auctioneers { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
