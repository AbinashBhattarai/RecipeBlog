using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
