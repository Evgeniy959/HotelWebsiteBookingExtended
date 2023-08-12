using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace HotelWebsiteBooking.Service.CommentService
{
    public class DbDaoComment : IDaoComment
    {
        private readonly AppDbContext _context;

        public DbDaoComment(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Client client, string content)
        {
            var comment = new Comment
            {
                Name = client.Name,
                Email = client.Email,
                Content = content,
                Date = DateTime.Now,
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentAsync(string name, string email, string content)
        {
            var comment = new Comment
            {
                Name = name,
                Email = email,
                Content = content,
                Date = DateTime.Now,
            };
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
    }
}
