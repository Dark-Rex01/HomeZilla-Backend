using Final.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Final.Model.Auth
{
    public class RegisterRequest : IValidatableObject
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        public string UserRole { get; set; } = String.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string? Password { get; set; }
        [Required]
        public long MobileNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(UserRole, true, out Role result))
            {
                yield return new ValidationResult("Invalid Role type", new[] { nameof(UserRole) });
            }
            UserRole = result.ToString(); //normalize Type
        }
    }
}
