using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Details { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Project> Projects { get; set; } = [];
        public ICollection<Customer> Customers { get; set; } = [];
    }
}
