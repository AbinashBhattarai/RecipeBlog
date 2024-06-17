using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDto ToCompanyDto(this Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Code = company.Code,
                Details = company.Details,
            };
        }

        public static Company AsCompanyModel(this CreateCompanyDto dto)
        {
            return new Company
            {
                Code = dto.Code,
                Details = dto.Details
            };
        }
    }
}
