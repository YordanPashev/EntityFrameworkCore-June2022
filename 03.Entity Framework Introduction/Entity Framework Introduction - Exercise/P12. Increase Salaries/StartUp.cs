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
            string allEmployeesInfo = IncreaseSalaries(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string IncreaseSalaries(SoftUniContext softUniContext)
        {
            string[] departmentsForSalaryIncrease = new string[4]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employeesForSalaryIncrease = softUniContext.Employees
                                                .Where(e => departmentsForSalaryIncrease
                                                                        .Contains(e.Department.Name))                                              
                                                .OrderBy(e => e.FirstName)
                                                .ThenBy(e => e.LastName)
                                                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in employeesForSalaryIncrease)
            {
                employee.Salary = employee.Salary * 1.12m;
                softUniContext.SaveChanges();

                result.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            return result.ToString().TrimEnd();
        }
    }
}
