using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Details { get; set; }

        [StringLength(255)]
        public string? TechStack { get; set; }

        public int Duration { get; set; }
    }
}
