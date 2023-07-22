using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelAdmin.Helpers;
using HotelAdmin.Service.ClientService;

namespace HotelAdmin.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoClient _daoClient;

        public ClientController(AppDbContext context, IDaoClient daoClient)
        {
            _context = context;
            _daoClient = daoClient;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Clients.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoClient.IndexAsync(page).Result);
        }

        public IActionResult Details(int id)
        {
            if (_daoClient.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoClient.GetAsync(id).Result);
        }

        public IActionResult Edit(int id)
        {
            if (_daoClient.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoClient.GetAsync(id).Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client)
        {            
            if (ModelState.IsValid && _daoClient.UpdateAsync(client).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(client);

        }

        public IActionResult Delete(int id)
        {
            if (_daoClient.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoClient.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoClient.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
