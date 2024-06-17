using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.Repository.Interface
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project?> GetProjectById(int id);
        Task<Project> AddProject(Project project);
        Task<Project?> UpdateProject(int id, CreateUpdateProjectDto dto);
        Task<bool> DeleteProject(int id);
    }
}
