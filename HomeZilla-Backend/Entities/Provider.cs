using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Entities
{
    public class Provider
    {
        [Key]
        [Required]
        public  Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int MobileNumber { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }

        [ForeignKey("provider")]
        public Guid ProviderUserID { get; set; }
        public virtual Authentication? provider { get; set; }
        public string? ProfilePicture { get; set; }

        public virtual ICollection<ProviderServices>? ServiceId { get; set; }

    }
}
