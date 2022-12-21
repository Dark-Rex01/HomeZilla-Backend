using System.ComponentModel.DataAnnotations;

namespace Final.Model.Auth
{
    public class AuthenticateRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
