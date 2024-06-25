using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Models
{
    public class ClientRep
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Address { get; set; }
    }
}
