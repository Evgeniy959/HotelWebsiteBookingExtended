namespace HotelWebsiteBooking.Service.DateService
{
    public class DaoDate 
    {
        public DateTime end = new DateTime();
        public DateTime start = new DateTime();

        public void SaveDate(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
