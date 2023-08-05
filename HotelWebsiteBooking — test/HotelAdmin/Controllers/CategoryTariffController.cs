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

namespace HotelAdmin.Controllers
{
    public class CategoryTariffController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoCategoryTariff _daoCategoryTariff;

        public CategoryTariffController(AppDbContext context, IDaoCategoryTariff daoCategoryTariff)
        {
            _context = context;
            _daoCategoryTariff = daoCategoryTariff;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.TariffAdmins.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoCategoryTariff.IndexAsync(page).Result);
        }

        public IActionResult Details(int id)
        {
            if (_daoCategoryTariff.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoCategoryTariff.GetAsync(id).Result);
        }

        public IActionResult Add()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name");
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,CategoryId,TariffPlanId,Price")] CategoryTariff categoryTariff)
        {
            if (ModelState.IsValid && _daoCategoryTariff.AddAsync(categoryTariff).Result == true)
            {
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", categoryTariff.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Description", categoryTariff.TariffPlanId);
            return View(categoryTariff);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoryTariff = await _daoCategoryTariff.GetAsync(id);
            if (categoryTariff == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", categoryTariff.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Description", categoryTariff.TariffPlanId);
            return View(categoryTariff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,CategoryId,TariffPlanId,Price")] CategoryTariff categoryTariff)
        {
            if (ModelState.IsValid && await _daoCategoryTariff.UpdateAsync(categoryTariff) == true)
            {
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", categoryTariff.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Description", categoryTariff.TariffPlanId);
            return View(categoryTariff);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _daoCategoryTariff.GetAsync(id) == null)
            {
                return NotFound();
            }

            return View(await _daoCategoryTariff.GetAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoCategoryTariff.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
