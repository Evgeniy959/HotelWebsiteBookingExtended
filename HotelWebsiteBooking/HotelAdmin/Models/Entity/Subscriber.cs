using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Subscriber
    {
        public int Id { get; set; }
        /*[Required(ErrorMessage = "Fill in this field")]*/
        [MaxLength(255)]
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
