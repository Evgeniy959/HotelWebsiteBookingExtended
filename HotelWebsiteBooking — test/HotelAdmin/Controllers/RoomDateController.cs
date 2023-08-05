using HotelAdmin.Models;
using HotelAdmin.Service.ClientService;
using HotelAdmin.Service.RoomDateService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Controllers
{
    public class RoomDateController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoRoomDate _daoRoomDate;

        public RoomDateController(AppDbContext context, IDaoRoomDate daoRoomDate)
        {
            _context = context;
            _daoRoomDate = daoRoomDate;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Dates.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoRoomDate.IndexAsync(page).Result);
        }

        public IActionResult Delete(int id)
        {
            if (_daoRoomDate.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoomDate.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoRoomDate.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
