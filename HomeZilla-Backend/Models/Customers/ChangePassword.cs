using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Customers
{
    public class ChangePassword
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        [Compare("NewPassword")]
        public string? ConfirmNewPassword { get; set; }
    }
}
