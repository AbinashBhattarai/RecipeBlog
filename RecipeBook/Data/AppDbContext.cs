using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<Recipe> Recipe {  get; set; }
        public DbSet<LikeRecipe> LikeRecipe { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
