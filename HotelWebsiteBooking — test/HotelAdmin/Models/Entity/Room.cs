using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAdmin.Models.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public int Number { get; set; }

        // навигационные свойства
        public Category? Category { get; set; }
        public ICollection<RoomDate>? RoomDates { get; set; }
        public ICollection<Client>? Clients { get; set; }
        //public ICollection<RoomTariff>? Tariffs { get; set; }

        public Room()
        {
            Id = default;
            Number = default;
        }
    }
}
