using HotelAdmin.Models.Entity;

namespace HotelAdmin.Service.ClientService
{
    public interface IDaoClient
    {
        Task<List<Client>> IndexAsync(int page);
        Task<Client> GetAsync(int id);
        Task<bool> UpdateAsync(Client client);
        Task DeleteConfirmedAsync(int id);
    }
}
