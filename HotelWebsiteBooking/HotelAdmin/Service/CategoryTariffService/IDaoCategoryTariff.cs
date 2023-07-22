using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.CategoryTariffService
{
    public interface IDaoCategoryTariff
    {
        Task<List<CategoryTariff>> IndexAsync(int page);
        Task<bool> AddAsync(CategoryTariff categoryTariff);
        Task<CategoryTariff> GetAsync(int id);
        Task<bool> UpdateAsync(CategoryTariff categoryTariff);
        Task DeleteConfirmedAsync(int id);
    }
}
