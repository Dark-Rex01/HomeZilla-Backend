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
        public int? Cost { get; set; }

        [ForeignKey(nameof(customer))]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(provider))]
        public Guid? ProviderId { get; set; }
        // Relationship
        public virtual Customer? customer { get; set; }
        public virtual Provider? provider { get; set; }

    }
}
