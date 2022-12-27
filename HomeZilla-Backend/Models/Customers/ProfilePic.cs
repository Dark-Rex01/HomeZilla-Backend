using HomeZilla_Backend.Helpers;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Customers
{
    public class ProfilePic
    {
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Pick an Image")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile? File { get; set; }
    }
}
