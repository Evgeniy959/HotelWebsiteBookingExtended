using System.ComponentModel.DataAnnotations;

namespace HotelWebsiteBooking.Models.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Comment()
        {
            Name = "";
            Email = "";
            Content = "";
            Date = DateTime.Now;

        }
    }
}
