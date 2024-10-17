using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
