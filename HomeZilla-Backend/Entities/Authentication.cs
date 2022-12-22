using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class Authentication
    {
        [Key]
        public Guid AuthId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public Role UserRole { get; set; } 
        
        //Password Reset

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? PasswordHash { get; set; }

        //OTP verification

        public int? OTP { get; set; }
        public DateTime? OTPExpiresAt { get; set; }

        //Verification

        public DateTime? VerifiedAt { get; set; }
        public DateTime? PasswordResetAt { get; set; }
        public bool IsVerified { get; set; }

        // Relationship

        public virtual Customer? Customer { get; set; }
        public virtual Provider? Provider { get; set; }
    }
}
