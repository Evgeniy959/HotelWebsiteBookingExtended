using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int TariffId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
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
