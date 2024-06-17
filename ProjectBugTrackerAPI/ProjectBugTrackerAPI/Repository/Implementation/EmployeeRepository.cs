using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _context.Employee.FindAsync(id);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee?> UpdateEmployee(int id, CreateUpdateEmployeeDto dto)
        {
            var result = await _context.Employee.FindAsync(id);
            if (result == null)
            {
                return null;
            }

            result.Name = dto.Name;
            result.Email = dto.Email;
            result.Phone = dto.Phone;
            result.Adress = dto.Address;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            int result = 0;
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return (result > 0);
            }
            _context.Remove(employee);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<Employee?> CheckEmployeeEmail(string email)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(c => c.Email == email);
            return employee;
        }

        public async Task<Employee?> CheckEmployeePhone(string phone)
        {
            var employee = await _context.Employee
                .FirstOrDefaultAsync(c => c.Phone == phone);
            return employee;
        }

        public string GetNewEmployeeCode()
        {
            var lastEmployee = _context.Employee
                                    .OrderByDescending(e => e.Id)
                                    .FirstOrDefault();
               
            string employeeCode = "";
            if (lastEmployee == null)
            {
                employeeCode = "EMP001";
            }
            else
            {
                int startIndex = 3;
                int lastCode = int.Parse(lastEmployee.Code.Substring(startIndex));
                employeeCode = $"EMP{lastCode + 1:D3}";

            }
            return employeeCode;
        }
    }
}
