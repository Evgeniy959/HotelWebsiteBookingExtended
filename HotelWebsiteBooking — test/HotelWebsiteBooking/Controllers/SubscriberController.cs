using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.EmailService;
using System.Text;

namespace HotelWebsiteBooking.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IEmailSender _emailSender;

        public SubscriberController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult NotSubscribe(Subscriber person)
        {
            return View(person);
        }

        [HttpPost]
        public IActionResult Subscribe(Subscriber person)
        {
            if (_emailSender.SubscribeAsync(person).Result == true)
            {
                return View();
            }
            else return View("NotSubscribe", person);
        }
    }
}
