using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelWebsiteBooking.Models.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Square { get; set; }
        [Column("Persons_count")]
        public int PersonsCount { get; set; }
        public string Path { get; set; }

        public ICollection<Room>? Rooms { get; set; }
        public ICollection<CategoryTariff>? TariffAdmins { get; set; }

        public Category()
        {
            Id = default;
            Photo = "";
            Name = "";
            Square = "";
            PersonsCount = default;
            Path = "";
        }
    }
}
