using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.DateService;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelWebsiteBooking.Service.RoomService;
using Stripe;

namespace HotelWebsiteBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DaoDate _date;
        private readonly IDaoRoom _daoRoom;
        private readonly DaoGuest _guest;

        public RoomController(AppDbContext context, DaoDate date, IDaoRoom daoRoom, DaoGuest guest)
        {
            _context = context;
            _date = date;
            _daoRoom = daoRoom;
            _guest = guest;
        }

        public async Task<IActionResult> Rooms()
        {
            return (await _context.Rooms.ToListAsync()).Any() ? 
                          View(_daoRoom.RoomsAsync().Result) : NotFound();

        }


        [HttpPost]
        public IActionResult Search(DateTime start, DateTime end, int count)
        {
            ViewBag.Days = end.Subtract(start).Days;
            ViewBag.Guest = count;
            var rooms = _daoRoom.SearchAsync(start, end, count).Result;
            return rooms.Any() ? View(rooms) : RedirectToAction("NotFind");
        }

        public IActionResult Tariff(int? id)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            ViewBag.Guest = _guest.GuestCount;
            ViewBag.Days = _date.end.Subtract(_date.start).Days;
            var room = _daoRoom.GetRoomAsync(id).Result;
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        public IActionResult Booking(int? roomId, int tariffId, int price, string tariff, Client client)
        {
            ViewBag.Start = _date.start.ToShortDateString();
            ViewBag.End = _date.end.ToShortDateString();
            ViewBag.Price = price;
            ViewBag.Guest = _guest.GuestCount;
            ViewBag.Days = _date.end.Subtract(_date.start).Days;
            ViewBag.Tariff = tariff;
            var room = _daoRoom.GetRoomAsync(roomId).Result;
            client.Room = room;
            client.TariffId = tariffId;
            return View(client);
        }

        public IActionResult NotFind()
        {
            TempData["Status"] = "Неудачный поиск!";
            return View();
        }

        [HttpPost]
        public IActionResult AddBooking(RoomDate date, Client client, string content, string payment, int price, Order order, OrderPay orderPay)
        {            
            if (payment == "online")
            {
                ViewBag.Price = price;
                ViewBag.Сontent = content;
                return View("Pay", client);
            }
            else if (payment == "inPlace")
            {
                if (_daoRoom.AddBookingAsync(date, client, content, order, orderPay).Result == true)
                {
                    TempData["Status"] = "Успешное бронирование!";
                    return View("Info", client);
                }
                
            }
            TempData["Status"] = "Неудачное бронирование, попробуйте еще раз!";
            return View("InfoEror");
        }
        

    }
}
