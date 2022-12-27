using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Customers
{
    public class OrderResponse
    {
        public List<OrderData> Data { get; set; } = new List<OrderData>();
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int TotalPages { get; set; }
    }
}
