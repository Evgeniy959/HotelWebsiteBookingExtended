using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.PaymentService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelWebsiteBooking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Pay(string stripeEmail, string stripeToken, RoomDate date, Client client, string content, Order order, OrderPay orderPay, int price)
        {
            var result = await _paymentService.CreatePayAsync(stripeEmail, stripeToken, date, client, content, order, orderPay, price);
            if (result.Success)
            {
                TempData["Status"] = result.Message;
                return View("InfoPay", client);
            }
            TempData["Status"] = result.Message;
            return View("PayEror");
        }
    }
}
