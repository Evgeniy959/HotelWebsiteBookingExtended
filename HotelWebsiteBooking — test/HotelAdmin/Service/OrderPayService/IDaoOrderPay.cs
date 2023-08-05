using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.OrderService
{
    public interface IDaoOrderPay
    {
        Task<List<OrderPay>> IndexAsync(int page);
        Task<OrderPay> GetAsync(Guid id);
        Task DeleteConfirmedAsync(Guid id);
        Task<bool> UpdateAsync(OrderPay order);
        Task<bool> BookingSendAsync(Guid id);
    }
}
