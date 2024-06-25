using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Dtos
{
    public class ClientDto : CreateClientDto
    {
        public int Id { get; set; }
    }

    public class CreateClientDto : UpdateClientDto
    {
        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Code must be between 3 to 10 characters long")]
        public string Code { get; set; }
    }

    public class UpdateClientDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Details must not exceed 255 characters")]
        public string Details { get; set; }
    }
}
