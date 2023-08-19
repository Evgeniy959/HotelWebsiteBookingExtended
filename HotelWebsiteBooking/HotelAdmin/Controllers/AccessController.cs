using HotelAdmin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HotelAdmin.Models.Entity;
using System;

namespace HotelAdmin.Controllers
{
    public class AccessController : Controller
    {
        private readonly AppDbContext _context;

        public AccessController(AppDbContext context)
        {
            _context = context;
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
            var modelDb = await _context.LoginModels.FirstOrDefaultAsync(a => a.Email == loginModel.Email);
            if (modelDb != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, modelDb.Password);
                if (validPassword)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, modelDb.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, modelDb.Role.ToString())
                        /*new Claim(ClaimTypes.Name, loginModel.Email),
                        new Claim(ClaimTypes.Role, loginModel.Role.ToString())*/
                    };
                    //var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["ValidateMessege"] = "Invalid Email or Password";
            return View();
            
        }
    }
}
