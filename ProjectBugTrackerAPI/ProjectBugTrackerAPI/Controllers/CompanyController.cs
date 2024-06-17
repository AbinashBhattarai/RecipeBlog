using Microsoft.AspNetCore.Mvc;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger _logger;

        public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyController> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var result = await _companyRepository.GetAllCompanies();
                var companies = result.Select(c => c.ToCompanyDto())
                                .ToList();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCompanies)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompany([FromRoute] int id)
        {
            try
            {
                var company = await _companyRepository.GetCompanyById(id);
                if (company == null)
                {
                    return NotFound("Company Not Found");
                }
                return Ok(company.ToCompanyDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCompany)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var code = await _companyRepository.GetCompanyByCode(companyDto.Code);
                if (code != null)
                {
                    ModelState.AddModelError("Code", "Company code already in use");
                    return BadRequest(ModelState);
                }
                var company = companyDto.AsCompanyModel();
                var result = await _companyRepository.AddCompany(company);
                return CreatedAtAction(nameof(GetCompany), new { id = result.Id }, result.ToCompanyDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCompany)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCompany([FromRoute] int id, [FromBody] UpdateCompanyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _companyRepository.UpdateCompany(id, dto);

                if (result == null)
                {
                    return NotFound("Company Not Found");
                }
                return Ok(result.ToCompanyDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCompany)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCompany([FromRoute] int id)
        {
            try
            {
                bool result = await _companyRepository.DeleteCompany(id);

                if (!result)
                {
                    return NotFound("Company Not Found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCompany)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error, please try again");
            }
        }
    }
}
