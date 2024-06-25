using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client?> GetClientById(int id);
        Task<Client> AddClient(Client client);
        Task<Client?> UpdateClient(int id, UpdateClientDto dto);
        Task<bool> DeleteClient(int id);
        Task<Client?> GetClientByCode(string code);
    }
}
