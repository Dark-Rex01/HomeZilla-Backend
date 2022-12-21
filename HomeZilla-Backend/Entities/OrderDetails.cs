using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;
namespace Final.Entities
{
    public class OrderDetails
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime AppointmentFrom { get; set; }
        public DateTime AppointmentTo { get; set; }
        public ServiceList ServiceName { get; set; }
        public OrderStatus Status { get; set; } = 0;

        // Relationship
        public Customer? customerDetails { get; set; }
        public Provider? providerDetails { get; set; }

    }
}
