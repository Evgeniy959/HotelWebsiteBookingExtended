using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class LoginModel
    {
        [Key]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }

        public LoginModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
