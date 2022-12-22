using System.ComponentModel.DataAnnotations;

namespace Final.Entities
{
    public class ProviderServices
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public virtual Provider? Provider { get; set; }
        public ServiceList Service { get; set; }
        public int? Price { get; set; }

    }
}
