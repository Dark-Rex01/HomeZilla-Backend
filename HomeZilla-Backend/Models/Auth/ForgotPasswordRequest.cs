using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
    }
}
