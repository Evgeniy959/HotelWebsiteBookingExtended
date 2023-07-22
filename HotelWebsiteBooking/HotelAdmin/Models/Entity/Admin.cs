using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Admin
    {
        [Key]
        public string Email { get; set; }         
        public string Password { get; set; }

        public Admin()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
