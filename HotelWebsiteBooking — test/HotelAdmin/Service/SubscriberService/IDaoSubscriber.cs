using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.SubscriberService
{
    public interface IDaoSubscriber
    {
        Task<List<Subscriber>> IndexAsync(int page);
        Task<Subscriber> GetAsync(int id);
        Task DeleteConfirmedAsync(int id);
    }
}
