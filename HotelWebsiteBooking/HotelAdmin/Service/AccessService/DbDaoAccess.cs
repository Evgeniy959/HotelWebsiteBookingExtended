using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelAdmin.Service.AccessService
{
    public class DbDaoAccess : IDaoAccess
    {
        private readonly AppDbContext _context;

        public DbDaoAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LoginInfo> LoginAsync(LoginModel loginModel)
        {
            var modelDb = await _context.LoginModels.FirstOrDefaultAsync(a => a.Email == loginModel.Email);
            if (modelDb == null)
            {
                return new LoginInfo
                {
                    Message = "Invalid Email",
                    Success = false
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, modelDb.Password))
            {
                return new LoginInfo
                {
                    Message = "Invalid Password",
                    Success = false
                };
            }
            return new LoginInfo
            {
                DataIdentity = AuthCookies(modelDb),
                Success = true
            };
        }


        private ClaimsIdentity AuthCookies(LoginModel loginModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginModel.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, loginModel.Role.ToString())
            };
            return new ClaimsIdentity(claims, "AppCookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            
        }
    }
}
