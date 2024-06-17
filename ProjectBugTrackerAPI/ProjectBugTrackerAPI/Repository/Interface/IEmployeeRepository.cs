using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(int id, CreateUpdateEmployeeDto dto);
        Task<bool> DeleteEmployee(int id);
        Task<Employee?> CheckEmployeeEmail(string email);
        Task<Employee?> CheckEmployeePhone(string phone);
        string GetNewEmployeeCode();
    }
}
