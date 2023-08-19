using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.CommentService
{
    public interface IDaoComment
    {
        Task AddCommentAsync(Client client, string content);
        Task AddCommentAsync(string name, string email, string content, string title);


    }
}
