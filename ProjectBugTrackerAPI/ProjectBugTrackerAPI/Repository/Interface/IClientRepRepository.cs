using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface IClientRepRepository
    {
        Task<IEnumerable<ClientRep>> GetAllClientReps();
        Task<ClientRep?> GetClientRepById(int id);
        Task<ClientRep> AddClientRep(ClientRep client);
        Task<ClientRep?> UpdateClientRep(int id, CreateUpdateClientRepDto dto);
        Task<bool> DeleteClientRep(int id);
        Task<ClientRep?> CheckClientRepEmail(string email);
    }
}
