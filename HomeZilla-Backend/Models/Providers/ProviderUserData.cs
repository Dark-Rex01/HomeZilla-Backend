﻿using Final.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeZilla_Backend.Models.Providers
{
    public class ProviderUserData
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
        public string? ProfilePicture { get; set; }
        public string Location { get; set; } = String.Empty;
        public string? Description { get; set; }
    }
}
