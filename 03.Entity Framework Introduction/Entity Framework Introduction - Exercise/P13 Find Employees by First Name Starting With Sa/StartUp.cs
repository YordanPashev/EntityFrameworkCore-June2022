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
            string allEmployeesInfo = GetEmployeesByFirstNameStartingWithSa(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext softUniContext)
        {
            var eployeesStartingWithSa = softUniContext.Employees
                                                .Where(e => e.FirstName
                                                                .ToLower()
                                                                .StartsWith("sa"))
                                                .Select(e => new
                                                                  {
                                                                      e.FirstName,
                                                                      e.LastName,
                                                                      e.JobTitle,
                                                                      e.Salary
                                                                  })
                                                .OrderBy(e => e.FirstName)
                                                .ThenBy(e => e.LastName)
                                                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in eployeesStartingWithSa)
            {             
                result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return result.ToString().TrimEnd();
        }
    }
}
