using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HotelAdmin.Service.CategoryService
{
    public class DbDaoCategory : IDaoCategory
    {
        private readonly AppDbContext _context;

        public DbDaoCategory(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(Category category, IFormFile photo)
        {
            try
            {
                category.Photo = await FileUploadHelper.Upload(photo);
            }
            catch (Exception) { }
            
            _context.Categorys.Add(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteConfirmedAsync(int id)
        {
            var category = await _context.Categorys.FindAsync(id);
            if (category != null)
            {
                _context.Categorys.Remove(category);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            await _context.TariffAdmins.LoadAsync();
            var category = await _context.Categorys
                .SingleOrDefaultAsync(m => m.Id == id);

            return category;
        }

        public async Task<List<Category>> IndexAsync(int page)
        {
            await _context.TariffAdmins.LoadAsync();
            var category = await _context.Categorys.ToListAsync();
            List<Category> list = new List<Category>();
            int TotalPages = (int)Math.Ceiling(category.Count / 10.0);

            if (!category.Any())
            {
                return category;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < category.Count; i++)
                {
                    list.Add(category[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(category[i]);
                }
                return list;
            }
        }

        public async Task<bool> UpdateAsync(Category category, IFormFile photo)
        {
            try
            {
                category.Photo = await FileUploadHelper.Upload(photo);
            }
            catch (Exception) { }
            
            _context.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
