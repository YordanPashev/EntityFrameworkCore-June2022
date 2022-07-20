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
            string allEmployeesInfo = GetEmployee147(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetEmployee147(SoftUniContext softUniContext)
        {
            int id = 147;
            var employeesInfo = softUniContext.Employees
                                                .Where(e => e.EmployeeId == id)
                                                .Select(e => new
                                                 {
                                                    e.FirstName,
                                                    e.LastName,
                                                    e.JobTitle,
                                                    e.EmployeesProjects,
                                                    Projects = e.EmployeesProjects
                                                                    .Select(p => p.Project.Name)                                                          
                                                 })
                                                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in employeesInfo)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

                foreach (var project in employee.Projects.OrderBy(p => p))
                {
                    result.AppendLine($"{project}");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
