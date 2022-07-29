using SoftUni;

namespace SoftUni.Infrastructure.DTOs
{
    public class EmployeeDTO
    {
        public EmployeeDTO() { }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get => $"{this.FirstName} {this.LastName}"; }

        public string? JobTitle { get; set; }

        public Department? Department { get; set; }

        public string? DepartmentName { get => this.Department?.Name; }
    }
}
