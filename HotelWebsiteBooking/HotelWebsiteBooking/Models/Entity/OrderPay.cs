using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelWebsiteBooking.Models.Entity
{
    public class OrderPay
    {
        [Key]
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
        public string? BookingNumber { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }

        public OrderPay()
        {
            Status = string.Empty;
            BookingNumber = string.Empty;
        }
    }
}
