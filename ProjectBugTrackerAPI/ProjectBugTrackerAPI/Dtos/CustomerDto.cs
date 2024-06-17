using System.ComponentModel.DataAnnotations;

namespace ProjectBugTrackerAPI.Dtos
{
    public class CustomerDto : CreateUpdateCustomerDto
    {
        public int Id { get; set; }
    }

    public class CreateUpdateCustomerDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name must not exceed 255 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email must not exceed 50 characters")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number must be 10 characters")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Address must not exceed 50 characters")]
        public string Address { get; set; }
    }
}
