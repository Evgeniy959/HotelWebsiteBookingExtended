using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.OrderService;
using HotelAdmin.Service.RoomDateService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Controllers
{
    public class OrderPayController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoOrderPay _daoOrder;

        public OrderPayController(AppDbContext context, IDaoOrderPay daoOrder)
        {
            _context = context;
            _daoOrder = daoOrder;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.OrderPays.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoOrder.IndexAsync(page).Result);
        }

        public IActionResult Details(Guid id)
        {
            if (_daoOrder.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoOrder.GetAsync(id).Result);
        }

        public IActionResult Edit(Guid id)
        {
            if (_daoOrder.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoOrder.GetAsync(id).Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderPay order)
        {
            if (ModelState.IsValid && _daoOrder.UpdateAsync(order).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(order);

        }

        public IActionResult Delete(Guid id)
        {
            if (_daoOrder.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoOrder.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _daoOrder.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BookingSend(Guid id)
        {
            await _daoOrder.BookingSendAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
