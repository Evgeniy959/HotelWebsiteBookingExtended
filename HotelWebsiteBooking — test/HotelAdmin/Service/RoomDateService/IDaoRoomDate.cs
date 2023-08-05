using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.RoomDateService
{
    public interface IDaoRoomDate
    {
        Task<List<RoomDate>> IndexAsync(int page);
        Task<RoomDate> GetAsync(int id);
        Task DeleteConfirmedAsync(int id);
    }
}
