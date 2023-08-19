using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Service.ClientService
{
    public class DbDaoClient : IDaoClient
    {
        private readonly AppDbContext _context;

        public DbDaoClient(AppDbContext context) 
        {
            _context = context;
        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                var dates = await _context.Dates.Where(d => d.ClientId == id).ToListAsync();
                _context.Dates.RemoveRange(dates);
                var orders = await _context.Orders.Where(o => o.ClientId == id).ToListAsync();
                _context.Orders.RemoveRange(orders);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetAsync(int id)
        {
            await _context.Rooms.LoadAsync();
            await _context.Categorys.LoadAsync();
            await _context.TariffAdmins.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var client = await _context.Clients
                .SingleOrDefaultAsync(m => m.Id == id);

            return client;
        }

        public async Task<List<Client>> IndexAsync(int page)
        {
            await _context.Rooms.LoadAsync();
            var clients = await _context.Clients.ToListAsync();
            List<Client> list = new List<Client>();
            int TotalPages = (int)Math.Ceiling(clients.Count / 10.0);

            if (!clients.Any())
            {
                return clients;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < clients.Count; i++)
                {
                    list.Add(clients[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(clients[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(Client client)
        {
            try 
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
