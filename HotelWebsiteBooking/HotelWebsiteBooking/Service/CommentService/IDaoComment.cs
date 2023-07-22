using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.CommentService
{
    public interface IDaoComment
    {
        Task AddCommentAsync(Client client, string content);
        
    }
}
