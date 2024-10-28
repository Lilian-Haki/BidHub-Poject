using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BidHub_Poject.Validation
{
    public class PasswordStrength: ValidationAttribute
    {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                var password = value as string;

                if (string.IsNullOrEmpty(password))
                    return new ValidationResult("Password is required.");

                if (password.Length < 8 || !Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]"))
                {
                    return new ValidationResult("Password must be at least 8 characters long and contain at least one special character.");
                }

                return ValidationResult.Success;
            }
        }
    }