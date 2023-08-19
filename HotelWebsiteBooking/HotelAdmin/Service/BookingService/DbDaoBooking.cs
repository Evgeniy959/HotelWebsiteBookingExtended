using HotelAdmin.Models;
using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.BookingService
{
    public class DbDaoBooking : IDaoBooking
    {
        //private readonly AppDbContext _context;

        /*public DbDaoBooking(AppDbContext context)
        {
            _context = context;
        }*/

        public async Task<Booking> AddBookingAsync()
        {
            DateTime beginDate = new DateTime(2023, 7, 29);
            DateTime currentDate = DateTime.Now;
            long countTicks = currentDate.Ticks - beginDate.Ticks;
            var booking = new Booking
            {
                //OrderId = id,
                Number = countTicks.ToString(),
                Date = DateTime.Now,
            };
            /*await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();*/
            return booking;
        }

        public async Task<BookingPay> AddBookingPayAsync(Guid id)
        {
            DateTime beginDate = new DateTime(2023, 7, 29);
            DateTime currentDate = DateTime.Now;
            long countTicks = currentDate.Ticks - beginDate.Ticks;
            var bookingPay = new BookingPay
            {
                OrderPayId = id,
                Id = countTicks.ToString(),
                Date = DateTime.Now,
            };
            /*await _context.BookingPays.AddAsync(booking);
            await _context.SaveChangesAsync();*/
            return bookingPay;
        }
    }
}
