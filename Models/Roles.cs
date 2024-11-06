using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Roles : IdentityRole<Guid>
    {
        //[Key]
        //public Guid RoleId { get; set; }
        public string Role { get; set; }
        public string RoleDescription { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }

    }
}
