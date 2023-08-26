using System.ComponentModel.DataAnnotations;

namespace HotelWebsiteBooking.Models.Entity
{
    public class CategoryTariff
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TariffPlanId { get; set; }
        public int Price { get; set; }

        // навигационные свойства
        public Category? Category { get; set; }
        public TariffPlan? TariffPlan { get; set; }
        public ICollection<Client>? Clients { get; set; }
    }
}
