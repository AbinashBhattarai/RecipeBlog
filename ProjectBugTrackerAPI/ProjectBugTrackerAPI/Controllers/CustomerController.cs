using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _customerRepository.GetAllCustomers();
                var customers = result.Select(c => c.ToCustomerDto()).ToList();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCustomers)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer == null)
                {
                    return NotFound("Customer Not Fount");
                }
                return Ok(customer.ToCustomerDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCustomer)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateUpdateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _customerRepository.CheckCustomerEmail(customerDto.Email);
                if (email != null)
                {
                    ModelState.AddModelError("Code", "Email already in use");
                    return BadRequest(ModelState);
                }
                var customer = customerDto.AsCustomerModel();
                var result = await _customerRepository.AddCustomer(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = result.Id }, result.ToCustomerDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCustomer)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, CreateUpdateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var email = await _customerRepository.CheckCustomerEmail(customerDto.Email);
                if (email != null && email.Id != id)
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return BadRequest(ModelState);
                }
                var result = await _customerRepository.UpdateCustomer(id, customerDto);

                if (result == null)
                {
                    return NotFound("Customer Not Found");
                }
                return Ok(result.ToCustomerDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCustomer)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            try
            {
                bool result = await _customerRepository.DeleteCustomer(id);

                if (!result)
                {
                    return NotFound("Customer Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCustomer)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}
