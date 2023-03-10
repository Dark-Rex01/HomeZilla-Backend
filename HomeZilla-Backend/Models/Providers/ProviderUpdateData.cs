using HomeZilla_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Providers
{
    public class ProviderUpdateData
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }

        public long? MobileNumber { get; set; }
        public Location Location { get; set; }
        public string? Description { get; set; }
    }
}
