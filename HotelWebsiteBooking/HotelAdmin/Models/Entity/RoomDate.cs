namespace HotelAdmin.Models.Entity
{
    public class RoomDate
    {
        public int Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int RoomId { get; set; }
        public int? ClientId { get; set; }

        // навигационные свойства
        public Room? Room { get; set; }
        public Client? Client { get; set; }
    }
}
