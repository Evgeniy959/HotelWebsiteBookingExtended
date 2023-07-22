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
using HotelAdmin.Service.RoomService;

namespace HotelAdmin.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoRoom _daoRoom;

        public RoomController(AppDbContext context, IDaoRoom daoRoom)
        {
            _context = context;
            _daoRoom = daoRoom;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Rooms.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoRoom.IndexAsync(page).Result);
        }

        public IActionResult Details(int id)
        {
            if (_daoRoom.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoom.GetAsync(id).Result);
        }

        public IActionResult Add(Room room)
        {
            ViewBag.Categorys = new SelectList(_context.Categorys, "Id", "Name");
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Room room, RoomDate date)
        {
            if (ModelState.IsValid && _daoRoom.AddAsync(room, date).Result == true)
            {               
                return RedirectToAction("Index");
            }
            //else
            //{
                ViewBag.Categorys = new SelectList(_context.Categorys, "Id", "Name", room.CategoryId);
                TempData["Status"] = "Room exsist!";
                return View(room);
            //}
            
        }

        public IActionResult Edit(int id)
        {
            var room = _daoRoom.GetAsync(id).Result;
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.Categorys = new SelectList(_context.Categorys, "Id", "Name", room.CategoryId);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Number,CategoryId")] Room room)
        {
            if (ModelState.IsValid && await _daoRoom.UpdateAsync(room) == true)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categorys = new SelectList(_context.Categorys, "Id", "Name", room.CategoryId);
            return View(room);

        }

        public IActionResult Delete(int id)
        {
            if (_daoRoom.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoom.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoRoom.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
