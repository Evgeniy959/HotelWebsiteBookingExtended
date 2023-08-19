using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class BookingPay
    {
        [Key]
        public string Id { get; set; }
        [Key]
        public Guid OrderPayId { get; set; }
        public DateTime Date { get; set; }

        public OrderPay? OrderPay { get; set; }

        public BookingPay()
        {
            Id = string.Empty;
        }

    }
}
