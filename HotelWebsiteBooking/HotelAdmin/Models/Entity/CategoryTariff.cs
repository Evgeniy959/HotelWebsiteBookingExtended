using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class CategoryTariff
    {
        public int Id { get; set; }
        [Display(Name = "Категория")]
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "Тарифный план")]
        [Required]
        public int TariffPlanId { get; set; }
        [Display(Name = "Цена")]
        [Required]
        public int Price { get; set; }

        // навигационные свойства
        public Category? Category { get; set; }
        public TariffPlan? TariffPlan { get; set; }
        public ICollection<Client>? Clients { get; set; }
    }
}
