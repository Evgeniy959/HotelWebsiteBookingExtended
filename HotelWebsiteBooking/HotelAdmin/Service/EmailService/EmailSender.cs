using HotelAdmin.Models;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;

namespace HotelAdmin.Service.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly AppDbContext _context;

        public EmailSender(AppDbContext context)
        {
            _context = context;
        }
        public async Task SendEmailAsync(string email, string subject, string content)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Hotel Admin", "evgpilipenkoY@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("New Client", email));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = content
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("evgpilipenkoY@yandex.ru", "tdutybq21@");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

    }
}
