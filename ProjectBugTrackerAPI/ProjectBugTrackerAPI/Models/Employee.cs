using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Adress { get; set; }
    }
}
