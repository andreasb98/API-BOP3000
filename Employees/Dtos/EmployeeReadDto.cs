namespace Employees.Dtos
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string EmploymentDate { get; set; }
    }
}