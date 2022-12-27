using Final.Entities;
using HomeZilla_Backend.Models.Search;
using System.ComponentModel.DataAnnotations;

namespace Final.Model.Search
{
    public class SearchResponse
    {
        [Required]
        public int CurrentPage { get; set; }
        [Required]
        public int TotalPages { get; set; }
        public IEnumerable<ProviderList> Data { get; set; } = Enumerable.Empty<ProviderList>();
    }
}
