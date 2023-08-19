using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelAdmin.Models.Entity
{
    public class Booking
    {
        //[Key]
        public string Number { get; set; }
        /*[PrimaryKey]
        public Guid OrderId { get; set; }*/
        public DateTime Date { get; set; }

        //public Order? Order { get; set; }

        public Booking()
        {
            Number = string.Empty;
        }

    }
}
