using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.TariffPlanService
{
    public interface IDaoTariffPlan
    {
        Task<List<TariffPlan>> IndexAsync(int page);
        Task<bool> AddAsync(TariffPlan tariffPlan);
        Task<TariffPlan> GetAsync(int id);
        Task<bool> UpdateAsync(TariffPlan tariffPlan);
        Task DeleteConfirmedAsync(int id);
    }
}
