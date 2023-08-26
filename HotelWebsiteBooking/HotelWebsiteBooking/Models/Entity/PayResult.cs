using System.Security.Claims;

namespace HotelWebsiteBooking.Models.Entity
{
    public class PayResult
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
    }
}
