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
using HotelAdmin.Service.TariffPlanService;

namespace HotelAdmin.Controllers
{
    public class TariffPlanController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoTariffPlan _daoTariffPlan;

        public TariffPlanController(AppDbContext context, IDaoTariffPlan daoTariffPlan)
        {
            _context = context;
            _daoTariffPlan = daoTariffPlan;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.TariffPlans.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoTariffPlan.IndexAsync(page).Result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Description")] TariffPlan tariffPlan)
        {
            if (ModelState.IsValid && _daoTariffPlan.AddAsync(tariffPlan).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(tariffPlan);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tariffPlan = await _daoTariffPlan.GetAsync(id);
            if (tariffPlan == null)
            {
                return NotFound();
            }

            return View(tariffPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Description")] TariffPlan tariffPlan)
        {
            if (ModelState.IsValid && await _daoTariffPlan.UpdateAsync(tariffPlan) == true)
            {
                return RedirectToAction("Index");
            }
            return View(tariffPlan);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _daoTariffPlan.GetAsync(id) == null)
            {
                return NotFound();
            }

            return View(await _daoTariffPlan.GetAsync(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoTariffPlan.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
