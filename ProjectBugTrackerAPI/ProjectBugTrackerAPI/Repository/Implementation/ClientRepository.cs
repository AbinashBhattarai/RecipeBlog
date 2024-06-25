using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _context.Client
                .OrderByDescending(id =>  id)
                .ToListAsync();
        }

        public async Task<Client?> GetClientById(int id)
        {
            return await _context.Client.FindAsync(id);
        }

        public async Task<Client> AddClient(Client client)
        {
            var result = await _context.Client.AddAsync(client);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client?> UpdateClient(int id, UpdateClientDto dto)
        {
            var result = await _context.Client.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            result.Details = dto.Details;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteClient(int id)
        {
            int result = 0;
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return (result > 0);
            }
            _context.Remove(client);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<Client?> GetClientByCode(string code)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(c => c.Code == code);
            return client;
        }
    }
}
