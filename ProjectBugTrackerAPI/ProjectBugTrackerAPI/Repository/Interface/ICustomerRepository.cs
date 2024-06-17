using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer?> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer?> UpdateCustomer(int id, CreateUpdateCustomerDto dto);
        Task<bool> DeleteCustomer(int id);
        Task<Customer?> CheckCustomerEmail(string email);
    }
}
