using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.RoomService
{
    public interface IDaoRoom
    {
        Task<List<Room>> IndexAsync(int page);
        Task<bool> AddAsync(Room room, RoomDate date);
        Task<Room> GetAsync(int id);
        Task<bool> UpdateAsync(Room room);
        Task DeleteConfirmedAsync(int id);
    }
}
