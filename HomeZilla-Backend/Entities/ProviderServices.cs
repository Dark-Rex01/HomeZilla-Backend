using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class ProviderServices
    {
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<Provider>? ProviderId { get; set; }
        public ServiceList? Service { get; set; }
        public int? Price { get; set; }

    }
}
