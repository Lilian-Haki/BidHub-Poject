using BidHub_Poject.Validation;
using System.ComponentModel.DataAnnotations;

namespace BidHub_Poject.DTO
{
    public class RegisterDTO
    {
        //[Required(ErrorMessage = "First Name Is required")]
        //public required string FirstName { get; set; }
        //[Required(ErrorMessage = "Last Name Is required")]
        //public required string LastName { get; set; }
        //[Required(ErrorMessage = "UserName Is required")]
        //public required string UserName { get; set; }
        //[Required(ErrorMessage = "Email Is required")]
        //public required string Email { get; set; }
        //[Required(ErrorMessage = "Password Is required")]
        //public required string Password { get; set; }
        //[Required(ErrorMessage = "Password Confirmation Is required")]
        //public required string ConfirmPassword { get; set; }
        //[Required(ErrorMessage ="Physical Address Is required")]
        //public string PhysicalAddress { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name  is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters")]
        public required string LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username can only contain letters or digits.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [PasswordStrength]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Physical Address is required")]
        public required string PhysicalAddress { get; set; }
        public string CompanyUrl { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits")]
        public required string PhoneNumber { get; set; }
        public int StaffNo { get; set; }
        public required string CompanyName { get; set; }
        public string Role { get; set; }

    }
}
