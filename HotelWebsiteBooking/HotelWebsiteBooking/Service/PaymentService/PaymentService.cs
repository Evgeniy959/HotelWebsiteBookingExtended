using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.RoomService;
using Stripe;
using Stripe.Checkout;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelWebsiteBooking.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IDaoRoom _daoRoom;

        public PaymentService(IDaoRoom daoRoom)
        {
            _daoRoom = daoRoom;
        }

        public async Task<PayResult> CreatePayAsync(string stripeEmail, string stripeToken, RoomDate date, Client client, string content, Order order, OrderPay orderPay, int price)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var orderPayId = Guid.NewGuid();
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = price * 100,
                Description = orderPayId.ToString(),
                Currency = "rub",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"Order number", orderPayId.ToString() },
                    {"Client email", client.Email },
                    {"Client name", client.Name },
                    {"Client surname", client.Surname }

                }
            });
            if (charge.Status == "succeeded")
            {
                orderPay.Id = orderPayId;
                orderPay.Status = "оплачено";
                if (await _daoRoom.AddBookingAsync(date, client, content, order, orderPay) == true)
                {
                    return new PayResult
                    {
                        Message = "Успешное бронирование!",
                        Success = true
                    };
                }
                else
                {
                    return new PayResult
                    {
                        Message = "Неудачное бронирование, попробуйте еще раз!",
                        Success = false
                    };
                }
            }
            else
            {
                return new PayResult
                {
                    Message = "Неудачная оплата, попробуйте еще раз!",
                    Success = false
                };
            }
        }
    }
}
