using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Auth
{
    public class VerifyAccount
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        public int? OTP { get; set; }
    }
}
