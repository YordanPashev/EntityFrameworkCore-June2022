namespace EFCoreBestPractices.Core.Contracts
{

    using EFCoreBestPractices.Core.Models;

    public interface IEmployeeService
    {
        Task<string> AddDepartment(ImportDepartmentModel model);

        Task<List<EmployeeModel>> GetEmployeesFromDepartment(int departmentId);
    }
}
