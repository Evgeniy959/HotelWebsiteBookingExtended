using System.ComponentModel.DataAnnotations;

namespace HotelWebsiteBooking.Models.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public int TariffId { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        // навигационные свойства
        public Room? Room { get; set; }
        //public RoomTariff? Tariff { get; set; }
        public CategoryTariff? Tariff { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<RoomDate>? RoomDates { get; set; }

        public Client()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }
}
