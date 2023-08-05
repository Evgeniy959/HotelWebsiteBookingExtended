using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebsiteBooking.Models.Entity
{
    public class Order
    {
        [Key]
        [Column("Number")]
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }

        public Order()
        {
            Status = string.Empty;
        }
    }
}
