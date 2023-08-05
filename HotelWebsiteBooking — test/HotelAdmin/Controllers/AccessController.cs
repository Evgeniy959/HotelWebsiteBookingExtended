using HotelAdmin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HotelAdmin.Models.Entity;

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
        public async Task<IActionResult> Login(Admin admin)
        {
            var adminDb = await _context.Admins.FirstOrDefaultAsync(a => a.Email == admin.Email);
            if (adminDb != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(admin.Password, adminDb.Password);
                if (validPassword)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.Email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["ValidateMessege"] = "Invalid Email of Password";
            return View();
            
        }
    }
}
