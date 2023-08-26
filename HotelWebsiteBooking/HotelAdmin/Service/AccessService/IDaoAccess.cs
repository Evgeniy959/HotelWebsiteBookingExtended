using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.AccessService
{
    public interface IDaoAccess
    {
        Task<LoginInfo> LoginAsync(LoginModel loginModel);
    }
}
