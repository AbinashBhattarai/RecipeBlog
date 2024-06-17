using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Code = employee.Code,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Adress,
            };
        }

        public static Employee AsEmployeeModel(this CreateUpdateEmployeeDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Adress = dto.Address,
            };
        }
    }
}
