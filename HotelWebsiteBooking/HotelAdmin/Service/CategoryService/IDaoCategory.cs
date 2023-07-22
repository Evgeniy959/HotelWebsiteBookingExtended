using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.CategoryService
{
    public interface IDaoCategory
    {
        Task<List<Category>> IndexAsync(int page);
        Task<bool> AddAsync(Category category, IFormFile photo);
        Task<Category> GetAsync(int id);
        Task<bool> UpdateAsync(Category category, IFormFile photo);
        Task DeleteConfirmedAsync(int id);
    }
}
