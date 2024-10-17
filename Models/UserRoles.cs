using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BidHub_Poject.Models
{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users User { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Roles Role { get; set; }
    }
}
