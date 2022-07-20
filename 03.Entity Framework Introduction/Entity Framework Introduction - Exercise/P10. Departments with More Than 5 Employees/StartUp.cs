namespace SoftUni
{

    using System;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;


    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = GetDepartmentsWithMoreThan5Employees(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext softUniContext)
        {
            var departmentsInfo = softUniContext.Departments
                                                .Where(e => e.Employees.Count > 5)
                                                .OrderBy(d => d.Employees.Count)
                                                .ThenBy(d => d.Name)
                                                .Select(d => new
                                                 {
                                                    ManagerJobTitle = d.Manager.JobTitle,
                                                    ManagerFirstName = d.Manager.FirstName,
                                                    ManagerLastName = d.Manager.LastName,
                                                    Employees = d.Employees.Select(e => new
                                                                                           {
                                                                                               e.FirstName,
                                                                                               e.LastName,
                                                                                               e.JobTitle
                                                                                           })
                                                    .OrderBy(e => e.FirstName)
                                                    .ThenBy(e => e.LastName)
                                                    .ToArray()
                                                 })
                                                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var department in departmentsInfo)
            {
                result.AppendLine($"{department.ManagerJobTitle} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)                                         
                {
                    result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
