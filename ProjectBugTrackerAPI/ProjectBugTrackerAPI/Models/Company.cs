using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Details { get; set; }
    }
}
