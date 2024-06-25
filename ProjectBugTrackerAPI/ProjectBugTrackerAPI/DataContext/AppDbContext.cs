using Microsoft.EntityFrameworkCore;
using ProjectBugTrackerAPI.Models;

namespace ProjectBugTrackerAPI.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientRep> ClientRep { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
