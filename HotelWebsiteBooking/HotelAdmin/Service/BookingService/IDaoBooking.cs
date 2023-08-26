using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.BookingService
{
    public interface IDaoBooking
    {
        Task<Booking> AddBookingAsync();
    }
}
