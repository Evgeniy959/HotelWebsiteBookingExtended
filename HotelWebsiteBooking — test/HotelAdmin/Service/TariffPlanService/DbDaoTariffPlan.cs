using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.CategoryService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HotelAdmin.Service.TariffPlanService
{
    public class DbDaoTariffPlan : IDaoTariffPlan
    {
        private readonly AppDbContext _context;

        public DbDaoTariffPlan(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(TariffPlan tariffPlan)
        {            
            _context.TariffPlans.Add(tariffPlan);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var tariffPlan = await _context.TariffPlans.FindAsync(id);
            if (tariffPlan != null)
            {
                _context.TariffPlans.Remove(tariffPlan);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<TariffPlan> GetAsync(int id)
        {
            //await _context.TariffAdmins.LoadAsync();
            var tariffPlan = await _context.TariffPlans
                .SingleOrDefaultAsync(m => m.Id == id);

            return tariffPlan;
        }

        public async Task<List<TariffPlan>> IndexAsync(int page)
        {
            //await _context.TariffAdmins.LoadAsync();
            var tariffPlan = await _context.TariffPlans.ToListAsync();
            List<TariffPlan> list = new List<TariffPlan>();
            int TotalPages = (int)Math.Ceiling(tariffPlan.Count / 10.0);

            if (!tariffPlan.Any())
            {
                return tariffPlan;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < tariffPlan.Count; i++)
                {
                    list.Add(tariffPlan[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(tariffPlan[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(TariffPlan tariffPlan)
        {
            
            _context.Update(tariffPlan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
