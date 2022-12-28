using Final.Entities;

namespace HomeZilla_Backend.Models.Providers
{
    public class GetService
    {
        public Guid Id { get; set; }
        public ServiceList Service { get; set; }
        public int? Price { get; set; }
    }
}
