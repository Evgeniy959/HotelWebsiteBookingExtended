using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string content);
        Task<bool> SubscribeAsync(Subscriber person);
    }
}
