using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Dtos
{
    public class ProjectDto : CreateUpdateProjectDto
    {
        public int Id { get; set; }
    }

    public class CreateUpdateProjectDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Detail must not exceed 255 characters")]
        public string Details { get; set; }

        [StringLength(255, ErrorMessage = "Tech Stack must not exceed 255 characters")]
        public string? TechStack { get; set; }

        [Required(ErrorMessage = "Provide duration in months")]
        public int Duration { get; set; }
    }
}
