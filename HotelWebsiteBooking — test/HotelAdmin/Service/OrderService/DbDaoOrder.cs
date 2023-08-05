using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Service.OrderService
{
    public class DbDaoOrder : IDaoOrder
    {
        private readonly AppDbContext _context;

        public DbDaoOrder(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteConfirmedAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            await _context.Clients.LoadAsync();
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var order = await _context.Orders
                .SingleOrDefaultAsync(m => m.Id == id);

            return order;
        }

        public async Task<List<Order>> IndexAsync(int page)
        {
            await _context.Clients.LoadAsync();
            var orders = await _context.Orders.ToListAsync();
            List<Order> list = new List<Order>();
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

        public async Task<bool> UpdateAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
