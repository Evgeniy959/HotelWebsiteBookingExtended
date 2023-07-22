using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.RoomService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelAdmin.Service.CategoryTariffService;
using HotelAdmin.Service.CategoryService;

namespace HotelAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoCategory _daoCategory;

        public CategoryController(AppDbContext context, IDaoCategory daoCategory)
        {
            _context = context;
            _daoCategory = daoCategory;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Categorys.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoCategory.IndexAsync(page).Result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Name,Photo,Square,PersonsCount")] Category category, IFormFile photo)
        {
            if (ModelState.IsValid && _daoCategory.AddAsync(category, photo).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoryTariff = await _daoCategory.GetAsync(id);
            if (categoryTariff == null)
            {
                return NotFound();
            }

            return View(categoryTariff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Photo,Square,PersonsCount")] Category category, IFormFile photo)
        {
            if (ModelState.IsValid && await _daoCategory.UpdateAsync(category, photo) == true)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _daoCategory.GetAsync(id) == null)
            {
                return NotFound();
            }

            return View(await _daoCategory.GetAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoCategory.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
