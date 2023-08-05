using HotelAdmin.Models;
using HotelAdmin.Service.OrderService;
using HotelAdmin.Service.SubscriberService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoSubscriber _daoSubscriber;

        public SubscriberController(AppDbContext context, IDaoSubscriber daoSubscriber)
        {
            _context = context;
            _daoSubscriber = daoSubscriber;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Subscribers.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoSubscriber.IndexAsync(page).Result);
        }

        public IActionResult Delete(int id)
        {
            if (_daoSubscriber.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoSubscriber.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoSubscriber.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
