using Final.Entities;
using System.ComponentModel.DataAnnotations;

namespace Final.Model.Search
{
    public class ProviderData
    {
        [Required]
        public Guid Id { get; set; }
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
        public List<ServiceData> ServiceData{ get; set; } = new List<ServiceData>();
    }
}
