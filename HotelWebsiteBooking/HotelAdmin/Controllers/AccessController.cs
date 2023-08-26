using HotelAdmin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HotelAdmin.Models.Entity;
using System;
using HotelAdmin.Service.AccessService;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelAdmin.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoAccess _daoAccess;

        public AccessController(AppDbContext context, IDaoAccess daoAccess)
        {
            _context = context;
            _daoAccess = daoAccess;
        }
        
        public IActionResult Login()
        {
            ClaimsPrincipal claimAdmin = HttpContext.User;
            if (claimAdmin.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _daoAccess.LoginAsync(loginModel);
                if (result.Success)
                {
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(result.DataIdentity));
                    return RedirectToAction("Index", "Home");
                }
                ViewData["ValidateMessege"] = result.Message;
                return View();
            }
            return View();
            
        }
    }
}
