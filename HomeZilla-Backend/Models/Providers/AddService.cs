using Final.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Providers
{
    public class AddService : IValidatableObject
    {
        public string Service { get; set; } = String.Empty; 
        public int? Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(Service, true, out ServiceList result))
            {
                yield return new ValidationResult("Invalid Role type", new[] { nameof(Service) });
            }
            Service = result.ToString(); //normalize Type
        }
    }
}
