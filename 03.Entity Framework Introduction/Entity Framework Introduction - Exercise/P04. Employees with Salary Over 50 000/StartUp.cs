namespace SoftUni
{

    using System;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;
    using Microsoft.EntityFrameworkCore.Query;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = GetEmployeesWithSalaryOver50000(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var allEmployees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50_000)
                .OrderBy(e => e.FirstName)
                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in allEmployees)
            {
                result.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
