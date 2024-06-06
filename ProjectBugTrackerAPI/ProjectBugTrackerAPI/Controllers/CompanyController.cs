using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.Data;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Mappers;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            var companies = await _context.Company
                .Select(c => c.ToCompanyDto())
                .ToListAsync();
            return Ok(companies);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Company>> GetCompanyById(int id)
        {
            var company = await _context.Company
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound("Product Not Fount");
            }
            return Ok(company.ToCompanyDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto createCompanyDto)
        {
            var company = createCompanyDto.ToCompanyModel();
            await _context.Company.AddAsync(company);
            await _context.SaveChangesAsync();
            return Ok("Product Added Successfully");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyDto dto)
        {
            var company = await _context.Company.FirstOrDefaultAsync(c => c.Id == id);

            if(company == null)
            {
                return NotFound("Product Not Found");
            }
            company.Code = dto.Code;
            company.Details = dto.Details;
            await _context.SaveChangesAsync();

            return Ok("Product Updated Successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Company.FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound("Product Not Found");
            }
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();

            return Ok("Product Deleted Successfully");
        }
    }
}
