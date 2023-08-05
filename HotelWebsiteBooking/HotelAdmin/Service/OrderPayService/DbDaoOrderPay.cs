using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using HotelAdmin.Service.EmailService;

namespace HotelAdmin.Service.OrderService
{
    public class DbDaoOrderPay : IDaoOrderPay
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public DbDaoOrderPay(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteConfirmedAsync(Guid id)
        {
            var order = await _context.OrderPays.FindAsync(id);
            if (order != null)
            {
                _context.OrderPays.Remove(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<OrderPay> GetAsync(Guid id)
        {
            await _context.Clients.LoadAsync();
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var order = await _context.OrderPays
                .SingleOrDefaultAsync(m => m.Id == id);

            return order;
        }

        public async Task<List<OrderPay>> IndexAsync(int page)
        {
            await _context.Clients.LoadAsync();
            var orders = await _context.OrderPays.ToListAsync();
            List<OrderPay> list = new List<OrderPay>();
            int TotalPages = (int)Math.Ceiling(orders.Count / 10.0);

            if (!orders.Any())
            {
                return orders;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < orders.Count; i++)
                {
                    list.Add(orders[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(orders[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(OrderPay order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BookingSendAsync(Guid id)
        {
            await _context.Clients.LoadAsync();
            OrderPay order = await GetAsync(id);
            /*Subscriber personContains = _context.Subscribers.FirstOrDefault(x => x.Email == person.Email);
            if (personContains == null)
            {
                person.Date = DateTime.Now;
                _context.Subscribers.AddAsync(person);
                await _context.SaveChangesAsync();*/
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"<h2>Поздравляем Вы подписаны на наши новости!</h2>");
                await _emailSender.SendEmailAsync(order.Client.Email, "Подписка на новости", stringBuilder.ToString());
                return true;
            /*}
            else return false;*/
            /*_context.Update(order);
            await _context.SaveChangesAsync();*/
            //return true;
        }
    }
}
