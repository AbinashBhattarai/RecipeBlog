using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.DataContext;
using ProjectBugTrackerAPI.Dtos;
using ProjectBugTrackerAPI.Models;
using ProjectBugTrackerAPI.Repository.Interface;

namespace ProjectBugTrackerAPI.Repository.Implementation
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task<Project?> GetProjectById(int id)
        {
            return await _context.Project.FindAsync(id);
        }

        public async Task<Project> AddProject(Project project)
        {
            var result = await _context.Project.AddAsync(project);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Project?> UpdateProject(int id, CreateUpdateProjectDto dto)
        {
            var result = await _context.Project.FindAsync(id);
            if (result == null)
            {
                return null;
            }

            result.Name = dto.Name;
            result.Details = dto.Details;
            result.TechStack = dto.TechStack;
            result.Duration = dto.Duration;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteProject(int id)
        {
            int result = 0;
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return (result > 0);
            }
            _context.Remove(project);
            result = await _context.SaveChangesAsync();
            return (result > 0);
        }
    }
}
