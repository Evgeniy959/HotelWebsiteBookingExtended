using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.CommentService
{
    public interface IDaoComment
    {
        Task<bool> AddAsync(Comment comment);
        Task<List<Comment>> IndexAsync(int page);
        Task<Comment> GetAsync(int id);
        Task DeleteConfirmedAsync(int id);
    }
}
