using HomeZilla_Backend.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Search
{
    public class ProviderList
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public long? MobileNumber { get; set; }
        public Location Location { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
