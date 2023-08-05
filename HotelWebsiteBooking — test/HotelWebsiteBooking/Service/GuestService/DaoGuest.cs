namespace HotelWebsiteBooking.Service.DateService
{
    public class DaoGuest
    {
        public int GuestCount { get; set; }

        public void SaveGuest(int guestCount)
        {
            GuestCount = guestCount;
        }
    }
}
