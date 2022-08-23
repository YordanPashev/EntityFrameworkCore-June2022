namespace EFCoreBestPractices.Core.Services
{

    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    using Core.Models;
    using Core.Contracts;
    using Infrastructure.Data;
    using Infrastructure.Data.Common;
    using Infrastructure.Data.Models;

    public class EmployeeService : IEmployeeService
    {
        private readonly ISoftUniRepository repo;
        private SoftUniContext context;

        public EmployeeService(ISoftUniRepository _repo, SoftUniContext _context)
        {
            repo = _repo;
            context = _context;
        }

        public async Task<string> AddDepartment(ImportDepartmentModel model)
        {
            if (IsValid(model))
            {
                Department department = new Department()
                {
                    Name = model.Name,
                    ManagerId = model.ManagerId
                };

                context.Departments.Add(department);
                context.SaveChanges();

                return $"You add new Department with name : {department.Name}";
            }

            return $"The Department name or Manager ID is not valid.";
        }

        public async Task<List<EmployeeModel>> GetEmployeesFromDepartment(int departmentId)
        {
            return await repo.AllReadonly<Employee>()
                .Where(e => e.DepartmentId == departmentId)
                .Select(e => new EmployeeModel()
                {
                    FirstName = e.FirstName,
                    HireDate = e.HireDate,
                    Id = e.EmployeeId,
                    JobTitle = e.JobTitle,
                    LastName = e.LastName,
                    MiddleName = e.MiddleName,
                    Salary = e.Salary
                }).ToListAsync();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
