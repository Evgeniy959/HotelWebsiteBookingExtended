using System.Security.Claims;

namespace HotelAdmin.Models.Entity
{
    public class LoginInfo
    {
        public string? Message { get; set; }
        public ClaimsIdentity? DataIdentity { get; set; }
        public bool Success { get; set; }
    }
}
