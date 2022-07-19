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
            string allEmployeesInfo = GetEmployeesFromResearchAndDevelopment(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            string nameOfTheDeparment = "Research and Development";
            var allEmployeeesInCurrDeparment = context.Employees
                .Where(e => e.Department.Name == nameOfTheDeparment)
                .Select(e => new { 
                                    e.FirstName, 
                                    e.LastName,
                                    e.Salary
                                 })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in allEmployeeesInCurrDeparment)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} from {nameOfTheDeparment} - ${employee.Salary:F2}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
