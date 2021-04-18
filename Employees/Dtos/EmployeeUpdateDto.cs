using System.ComponentModel.DataAnnotations;

namespace Employees.Dtos
{
    public class EmployeeUpdateDto
    {

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string EmploymentDate { get; set; }
    }
}