using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.CommentService;
using HotelAdmin.Service.OrderService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoComment _daocomment;

        public CommentController(AppDbContext context, IDaoComment daocomment)
        {
            _context = context;
            _daocomment = daocomment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Comments.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daocomment.IndexAsync(page).Result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([Bind("Id,Name,Email,Content,Date")] Comment comment )
        {
            if (_daocomment.AddAsync(comment).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        public IActionResult Delete(int id)
        {
            if (_daocomment.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daocomment.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daocomment.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
