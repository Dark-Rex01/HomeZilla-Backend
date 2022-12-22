using Final.Entities;

namespace Final.Model.Search
{
    public class ServiceData
    {
        public Guid Id { get; set; }
        public ServiceList? Service { get; set; }
        public int? Price { get; set; }
    }
}
