using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<Company?> GetCompanyById(int id);
        Task<Company> AddCompany(Company company);
        Task<Company?> UpdateCompany(int id, UpdateCompanyDto dto);
        Task<bool> DeleteCompany(int id);
        Task<Company?> GetCompanyByCode(string code);
    }
}
