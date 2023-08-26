using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Booking
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }

        public Booking()
        {
            Number = string.Empty;
        }

    }
}
