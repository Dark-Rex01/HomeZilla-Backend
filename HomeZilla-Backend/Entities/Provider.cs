using HomeZilla_Backend.Entities;
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
        public long MobileNumber { get; set; }
        public string? UserName { get; set; }
        public Location Location { get; set; } 
        public string? Description { get; set; }

        [ForeignKey("provider")]
        public Guid ProviderUserID { get; set; }
        public virtual Authentication? provider { get; set; }
        public string? ProfilePicture { get; set; }

        public virtual ICollection<ProviderServices> Service { get; set; } = new HashSet<ProviderServices>();
        public virtual ICollection<OrderDetails> OrderDeatils { get; set; } = new List<OrderDetails>();

    }
}
