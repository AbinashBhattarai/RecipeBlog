using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDto ToProjectDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Details = project.Details,
                TechStack = project.TechStack,
                Duration = project.Duration,
            };
        }

        public static Project AsProjectModel(this CreateUpdateProjectDto dto)
        {
            return new Project
            {
                Name = dto.Name,
                Details = dto.Details,
                TechStack = dto.TechStack,
                Duration = dto.Duration,
            };
        }
    }
}
