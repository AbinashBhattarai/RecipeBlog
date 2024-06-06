using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBugTrackerAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Details { get; set; }

        [Required]
        [MaxLength(255)]
        public string TechStack { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Employee> Employees { get; set; } = [];
        public ICollection<Customer> Customers { get; set; } = [];
    }
}
