using Final.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Customers
{
    public class UserData
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
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
