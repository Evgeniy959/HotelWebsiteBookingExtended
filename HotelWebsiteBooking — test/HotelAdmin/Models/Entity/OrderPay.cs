using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class OrderPay
    {
        [Key]
        [Column("Number")]
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int GuestCount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public Client? Client { get; set; }

        public OrderPay()
        {
            Status = string.Empty;
        }
    }
}
