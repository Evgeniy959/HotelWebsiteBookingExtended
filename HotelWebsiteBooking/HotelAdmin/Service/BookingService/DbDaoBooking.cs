using HotelAdmin.Models;
using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.BookingService
{
    public class DbDaoBooking : IDaoBooking
    {
        public async Task<Booking> AddBookingAsync()
        {
            DateTime beginDate = new DateTime(2023, 7, 29);
            DateTime currentDate = DateTime.Now;
            long countTicks = currentDate.Ticks - beginDate.Ticks;
            var booking = new Booking
            {
                Number = countTicks.ToString(),
                Date = DateTime.Now,
            };
            return booking;
        }

    }
}
