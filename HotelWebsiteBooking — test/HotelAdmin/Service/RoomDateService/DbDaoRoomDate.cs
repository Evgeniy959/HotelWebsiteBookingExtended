using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Service.RoomDateService
{
    public class DbDaoRoomDate : IDaoRoomDate
    {
        private readonly AppDbContext _context;

        public DbDaoRoomDate(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteConfirmedAsync(int id)
        {
            var roomDate = await _context.Dates.FindAsync(id);
            if (roomDate != null)
            {
                _context.Dates.Remove(roomDate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDate> GetAsync(int id)
        {
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            var roomDate = await _context.Dates
                .SingleOrDefaultAsync(m => m.Id == id);

            return roomDate;
        }

        public async Task<List<RoomDate>> IndexAsync(int page)
        {
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            var roomDates = await _context.Dates.ToListAsync();
            List<RoomDate> list = new List<RoomDate>();
            int TotalPages = (int)Math.Ceiling(roomDates.Count / 10.0);

            if (!roomDates.Any())
            {
                return roomDates;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < roomDates.Count; i++)
                {
                    list.Add(roomDates[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(roomDates[i]);
                }
                return list;
            }
        }
    }
}
