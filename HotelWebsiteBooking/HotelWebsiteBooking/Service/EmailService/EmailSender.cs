using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Text;

namespace HotelWebsiteBooking.Service.EmailService
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

            emailMessage.From.Add(new MailboxAddress("News Admin", "evgpilipenkoY@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("News Client", email));
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

        public async Task<bool> SubscribeAsync(Subscriber person)
        {
            Subscriber personContains = _context.Subscribers.FirstOrDefault(x => x.Email == person.Email);
            if (personContains == null)
            {
                person.Date = DateTime.Now;
                _context.Subscribers.AddAsync(person);
                await _context.SaveChangesAsync();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"<h2>Поздравляем Вы подписаны на наши новости!</h2>");
                await SendEmailAsync(person.Email, "Подписка на новости", stringBuilder.ToString());
                return true;
            }
            else return false;
        }
    }
}
