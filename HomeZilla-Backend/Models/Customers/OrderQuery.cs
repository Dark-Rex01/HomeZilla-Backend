using Final.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Customers
{
    public class OrderQuery : IValidatableObject
    {
        public int PageNumber { get; set; } = 1;
        public string? ServiceName { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ServiceName != null && ServiceName !="")
            {
                if (!Enum.TryParse(ServiceName, true, out ServiceList result))
                {
                    yield return new ValidationResult("Invalid Service Type", new[] { nameof(ServiceName) });
                }
                ServiceName = result.ToString(); //normalize Type
            }
            
        }
    }
}
