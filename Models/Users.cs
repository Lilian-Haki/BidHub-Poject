using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Users: IdentityUser<Guid>
    {
        //[Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsVerified { get; set; }

       
        public ICollection<Bidders> Bidders { get; set; }
        public ICollection<Auctioneers> Auctioneers { get; set; }
        public ICollection<BViewing> BViewings { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }

    }
}
