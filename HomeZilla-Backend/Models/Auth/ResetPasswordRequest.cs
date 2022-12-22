using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class ResetPasswordRequest
    {
        public int? OTP{ get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string? Password { get; set; }
        [Required]
        [Compare("Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string?  ConfirmPassword{ get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
    }
}
