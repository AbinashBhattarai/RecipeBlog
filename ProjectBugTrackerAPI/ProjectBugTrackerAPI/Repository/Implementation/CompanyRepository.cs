using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            return await _context.Company
                .OrderByDescending(id =>  id)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyById(int id)
        {
            return await _context.Company.FindAsync(id);
        }

        public async Task<Company> AddCompany(Company company)
        {
            var result = await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Company?> UpdateCompany(int id, UpdateCompanyDto dto)
        {
            var result = await _context.Company.FindAsync(id);
            if (result == null)
            {
                return null;
            }
            result.Details = dto.Details;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            int result = 0;
            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return (result > 0);
            }
            _context.Remove(company);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<Company?> GetCompanyByCode(string code)
        {
            var company = await _context.Company
                .FirstOrDefaultAsync(c => c.Code == code);
            return company;
        }
    }
}
