using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Service.SubscriberService
{
    public class DbDaoSubscriber : IDaoSubscriber
    {
        private readonly AppDbContext _context;

        public DbDaoSubscriber(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteConfirmedAsync(int id)
        {
            var subscriber = await _context.Subscribers.FindAsync(id);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Subscriber> GetAsync(int id)
        {
            var subscriber = await _context.Subscribers
                .SingleOrDefaultAsync(m => m.Id == id);

            return subscriber;
        }

        public async Task<List<Subscriber>> IndexAsync(int page)
        {
            var subscribers = await _context.Subscribers.ToListAsync();
            List<Subscriber> list = new List<Subscriber>();
            int TotalPages = (int)Math.Ceiling(subscribers.Count / 10.0);

            if (!subscribers.Any())
            {
                return subscribers;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < subscribers.Count; i++)
                {
                    list.Add(subscribers[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(subscribers[i]);
                }
                return list;
            }
        }
    }
}
