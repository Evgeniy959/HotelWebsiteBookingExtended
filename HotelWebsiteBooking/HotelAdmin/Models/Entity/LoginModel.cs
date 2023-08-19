using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class LoginModel
    {
        [Key]
        public string Email { get; set; }         
        public string Password { get; set; }
        public Role Role { get; set; }

        public LoginModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
