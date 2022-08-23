using EFCoreBestPractices.Core.Models;

namespace EFCoreBestPractices.Core.Contracts
{
    public interface IEmployeeService
    {
        Task<string> AddDepartment(ImportDepartmentModel model);

        Task<List<EmployeeModel>> GetEmployeesFromDepartment(int departmentId);
    }
}
