using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.DTO
{
    public class AssignRoleDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
