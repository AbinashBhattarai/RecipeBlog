using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class ClientRepRepository : IClientRepRepository
    {
        private readonly AppDbContext _context;
        public ClientRepRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientRep>> GetAllClientReps()
        {
            return await _context.ClientRep.ToListAsync();
        }

        public async Task<ClientRep?> GetClientRepById(int id)
        {
            return await _context.ClientRep.FindAsync(id);
        }

        public async Task<ClientRep> AddClientRep(ClientRep clientRep)
        {
            var result = await _context.ClientRep.AddAsync(clientRep);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ClientRep?> UpdateClientRep(int id, CreateUpdateClientRepDto dto)
        {
            var result = await _context.ClientRep.FindAsync(id);
            if (result == null)
            {
                return null;
            }

            result.Name = dto.Name;
            result.Email = dto.Email;
            result.Phone = dto.Phone;
            result.Address = dto.Address;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteClientRep(int id)
        {
            int result = 0;
            var clientRep = await _context.ClientRep.FindAsync(id);
            if (clientRep == null)
            {
                return (result > 0);
            }
            _context.Remove(clientRep);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<ClientRep?> CheckClientRepEmail(string email)
        {
            var clientRep = await _context.ClientRep
                .FirstOrDefaultAsync(c => c.Email == email);
            return clientRep;
        }
    }
}
