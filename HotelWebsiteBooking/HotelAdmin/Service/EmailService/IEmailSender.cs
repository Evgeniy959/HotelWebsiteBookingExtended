namespace HotelAdmin.Service.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string content);
        //Task<bool> SubscribeAsync(Subscriber person);
    }
}
