using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HotelAdmin.Service.CategoryTariffService
{
    public class DbDaoCategoryTariff : IDaoCategoryTariff
    {
        private readonly AppDbContext _context;

        public DbDaoCategoryTariff(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(CategoryTariff categoryTariff)
        {
            _context.TariffAdmins.Add(categoryTariff);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var categoryTariff = await _context.TariffAdmins.FindAsync(id);
            if (categoryTariff != null)
            {
                _context.TariffAdmins.Remove(categoryTariff);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryTariff> GetAsync(int id)
        {
            await _context.Categorys.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var categoryTariff = await _context.TariffAdmins
                .SingleOrDefaultAsync(m => m.Id == id);

            return categoryTariff;
        }

        public async Task<List<CategoryTariff>> IndexAsync(int page)
        {
            await _context.Categorys.LoadAsync();
            await _context.TariffPlans.LoadAsync();
            var categoryTariff = await _context.TariffAdmins.ToListAsync();
            List<CategoryTariff> list = new List<CategoryTariff>();
            int TotalPages = (int)Math.Ceiling(categoryTariff.Count / 10.0);

            if (!categoryTariff.Any())
            {
                return categoryTariff;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < categoryTariff.Count; i++)
                {
                    list.Add(categoryTariff[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(categoryTariff[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(CategoryTariff categoryTariff)
        {
            _context.Update(categoryTariff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
