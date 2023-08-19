using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.OrderService
{
    public interface IDaoOrder
    {
        Task<List<Order>> IndexAsync(int page);
        Task<Order> GetAsync(Guid id);
        Task DeleteConfirmedAsync(Guid id);
        Task<bool> UpdateAsync(Order order);
        Task<bool> BookingSendAsync(Guid id);
    }
}
