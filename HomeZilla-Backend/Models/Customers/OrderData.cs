using Final.Entities;

namespace HomeZilla_Backend.Models.Customers
{
    public class OrderData
    {
        public Guid Id { get; set; }
        public DateTime AppointmentFrom { get; set; }
        public DateTime AppointmentTo { get; set; }
        public ServiceList ServiceName { get; set; }
        public OrderStatus Status { get; set; }
        public int? Price { get; set; }

        public CustomerUserData? customer { get; set; }
    }
}
