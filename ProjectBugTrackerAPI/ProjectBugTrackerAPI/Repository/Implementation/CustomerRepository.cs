using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer?> UpdateCustomer(int id, CreateUpdateCustomerDto dto)
        {
            var result = await _context.Customer.FindAsync(id);
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

        public async Task<bool> DeleteCustomer(int id)
        {
            int result = 0;
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return (result > 0);
            }
            _context.Remove(customer);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<Customer?> CheckCustomerEmail(string email)
        {
            var customer = await _context.Customer
                .FirstOrDefaultAsync(c => c.Email == email);
            return customer;
        }
    }
}
