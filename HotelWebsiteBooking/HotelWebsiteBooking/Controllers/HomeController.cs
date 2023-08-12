using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.DateService;
using HotelWebsiteBooking.Service.RoomService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Diagnostics;

namespace HotelWebsiteBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IDaoRoom _daoRoom;
        private readonly DaoDate _date;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IDaoRoom daoRoom, DaoDate date)
        {
            _logger = logger;
            _context = context;
            _daoRoom = daoRoom;
            _date = date;
        }

        public IActionResult Index()
        {
            return (_context.Comments.ToList()).Any() ?
                          View(_context.Comments.ToList()) : View(new List<Comment>());
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Standard(int? idRoom, int? roomId)
        {            
            if (roomId!=null) 
            {
                ViewBag.Days = _date.end.Subtract(_date.start).Days;
                ViewData["Messege"] = "Booking";
                return View(_daoRoom.GetRoomAsync(roomId).Result);
            }
            return View(_daoRoom.GetRoomAsync(idRoom).Result);
        }

        public IActionResult StandardBig()
        {
            return View();
        }

        public IActionResult StandardGood()
        {
            return View();
        }

        public IActionResult StandardGoodBig()
        {
            return View();
        }

        public IActionResult SemiLuxury()
        {
            return View();
        }

        public IActionResult Luxury()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}