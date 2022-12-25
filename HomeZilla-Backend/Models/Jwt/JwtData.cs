using Final.Entities;
using System.ComponentModel.DataAnnotations;

namespace HomeZilla_Backend.Models.Jwt
{
    public class JwtData
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
    }
}
