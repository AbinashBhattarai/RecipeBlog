using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Implementation;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _employeeRepository.GetAllEmployees();
                var employees = result.Select(c => c.ToEmployeeDto()).ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetEmployees)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound("Employee Not Fount");
                }
                return Ok(employee.ToEmployeeDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetEmployee)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateUpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _employeeRepository.CheckEmployeeEmail(employeeDto.Email);
                var phone = await _employeeRepository.CheckEmployeePhone(employeeDto.Phone);
                if (email != null)
                {
                    ModelState.AddModelError("Code", "Email already in use");
                    return BadRequest(ModelState);
                }
                if (phone != null)
                {
                    ModelState.AddModelError("Phone", "Phone number already in use");
                    return BadRequest(ModelState);
                }
                var employee = employeeDto.AsEmployeeModel();

                employee.Code = _employeeRepository.GetNewEmployeeCode();

                var result = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result.ToEmployeeDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateEmployee)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, CreateUpdateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _employeeRepository.CheckEmployeeEmail(employeeDto.Email);
                var phone = await _employeeRepository.CheckEmployeePhone(employeeDto.Phone);
                if (email != null && email.Id != id)
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return BadRequest(ModelState);
                }
                if (phone != null && phone.Id != id)
                {
                    ModelState.AddModelError("Phone", "Phone number already in use");
                    return BadRequest(ModelState);
                }

                var result = await _employeeRepository.UpdateEmployee(id, employeeDto);

                if (result == null)
                {
                    return NotFound("Employee Not Found");
                }
                return Ok(result.ToEmployeeDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateEmployee)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            try
            {
                bool result = await _employeeRepository.DeleteEmployee(id);

                if (!result)
                {
                    return NotFound("Employee Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteEmployee)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}
