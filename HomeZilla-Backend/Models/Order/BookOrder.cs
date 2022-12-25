using Final.Entities;
using System.ComponentModel.DataAnnotations;

namespace Final.Model.Order
{
    public class BookOrder : IValidatableObject
    {
        public Guid? ProviderId { get; set; }
        public string? ServiceName { get; set; }
        public DateTime AppointmentFrom { get; set; }
        public DateTime AppointmentTo { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(ServiceName, true, out ServiceList result))
            {
                yield return new ValidationResult("Invalid status type", new[] { nameof(ServiceName) });
            }
            ServiceName = result.ToString(); //normalize Type
        }
    }
}
