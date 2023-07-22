using HotelAdmin.Helpers;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Service.CommentService
{
    public class DbDaoComment : IDaoComment
    {
        private readonly AppDbContext _context;

        public DbDaoComment(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Comment comment)
        {
            comment.Date = DateTime.Now;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task DeleteConfirmedAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            var comment = await _context.Comments
                .SingleOrDefaultAsync(m => m.Id == id);

            return comment;
        }

        public async Task<List<Comment>> IndexAsync(int page)
        {
            var comments = await _context.Comments.ToListAsync();
            List<Comment> list = new List<Comment>();
            int TotalPages = (int)Math.Ceiling(comments.Count / 10.0);

            if (!comments.Any())
            {
                return comments;
            }

            if (page == TotalPages)
            {
                for (var i = (page - 1) * 10; i < comments.Count; i++)
                {
                    list.Add(comments[i]);
                }
                return list;
            }
            else
            {
                for (var i = (page - 1) * 10; i < page * 10; i++)
                {
                    list.Add(comments[i]);
                }
                return list;
            }
        }
    }
}
