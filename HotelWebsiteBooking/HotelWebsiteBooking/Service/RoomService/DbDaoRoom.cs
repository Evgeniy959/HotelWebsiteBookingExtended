using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.CommentService;
using HotelWebsiteBooking.Service.DateService;
using Microsoft.EntityFrameworkCore;

namespace HotelWebsiteBooking.Service.RoomService
{
    public class DbDaoRoom : IDaoRoom
    {
        private readonly AppDbContext _context;
        private readonly DaoDate _date;       
        private readonly DaoGuest _guest;       
        private readonly IDaoComment _comment;       
        
        public DbDaoRoom(AppDbContext context, DaoDate date, DaoGuest guest, IDaoComment comment) 
        {
            _context = context;
            _date = date;
            _guest = guest;
            _comment = comment;
        }
        public async Task<bool> AddBookingAsync(RoomDate date, Client client, string content, Order order, OrderPay orderPay)
        {
            try
            {
                client.Start = _date.start;
                client.End = _date.end;
                _context.Add(client);
                await _context.SaveChangesAsync();
                if (orderPay.Status == "оплачено")
                {
                    orderPay.ClientId = client.Id;
                    orderPay.GuestCount = _guest.GuestCount;
                    orderPay.Date = DateTime.Now;
                    _context.Add(orderPay);
                }
                else
                {
                    order.ClientId = client.Id;
                    order.GuestCount = _guest.GuestCount;
                    order.Status = "без предоплаты";
                    order.Date = DateTime.Now;
                    _context.Add(order);
                }
                await _context.SaveChangesAsync();
                date.Start = _date.start;
                date.End = _date.end;
                date.RoomId = client.RoomId;
                date.ClientId = client.Id;
                var roomDate = await _context.Dates.FirstOrDefaultAsync(x => x.Start == null && x.RoomId == client.RoomId);
                if (roomDate != null)
                {
                    roomDate.Start = _date.start;
                    roomDate.End = _date.end;
                    roomDate.ClientId = client.Id;
                    _context.Dates.Update(roomDate);
                }
                else _context.Dates.Add(date);
                await _context.SaveChangesAsync();

                if (content != null)
                {
                    await _comment.AddCommentAsync(client, content);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Room>> RoomsAsync()
        {
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            var rooms = await _context.Rooms.ToListAsync();
            return rooms.GroupBy(g => g.CategoryId)
                        .Select(r => r.First())
                        .ToList();
        }

        public async Task<List<RoomDate>> SearchAsync(DateTime start, DateTime end, int count)
        {
            _date.SaveDate(start, end);
            _guest.SaveGuest(count);
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            var rooms = await _context.Dates.Where(x => x.Room.Category.PersonsCount >= count).ToListAsync();
            var roomDates = await _context.Dates.Where(x => x.Start >= start && x.End <= end && x.Room.Category.PersonsCount >= count
            || x.Start >= start && x.Start <= end && x.End >= end && x.Room.Category.PersonsCount >= count
            || x.Start <= start && x.End <= end && x.End >= start && x.Room.Category.PersonsCount >= count
            || x.Start <= start && x.End >= end && x.Room.Category.PersonsCount >= count).ToListAsync();
            
            List<RoomDate> list = new List<RoomDate>();

            foreach (var room in rooms)
            {
                foreach (var roomDate in roomDates)
                {
                    if (room.RoomId == roomDate.RoomId)
                    {
                        list.Add(room);
                    }
                }
            }
            foreach (var item in list)
            {
                if (rooms.Contains(item))
                {
                    rooms.Remove(item);
                }

            }
            return rooms.GroupBy(g => g.Room.CategoryId)
                        .Select(r => r.First())
                        .ToList();
        }
        
        public async Task<Room> GetRoomAsync(int? id)
        {
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);

            return room;
        }
    }
}
