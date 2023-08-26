using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<PayResult> CreatePayAsync(string stripeEmail, string stripeToken, RoomDate date, Client client, string content, Order order, OrderPay orderPay, int price);
    }
}
