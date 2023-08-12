using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.RoomService
{
    public interface IDaoRoom
    {
        Task<List<Room>> RoomsAsync();
        Task<List<RoomDate>> SearchAsync(DateTime start, DateTime end, int count);
        Task<bool> AddBookingAsync(RoomDate date, Client client, string content, Order order, OrderPay orderPay);
        Task<Room> GetRoomAsync(int? id);
    }
}
