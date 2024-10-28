using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace BidHub_Poject.Models
{
    public class UserRoles 
    {
        [Key]
        public int UserRoleId { get; set; }

        // [ForeignKey("User")]
        public int UserId { get; set; }
        public Users User { get; set; }

        //[ForeignKey("Role")]
        public int RoleId { get; set; }
        public Roles Role { get; set; }


    }
}
