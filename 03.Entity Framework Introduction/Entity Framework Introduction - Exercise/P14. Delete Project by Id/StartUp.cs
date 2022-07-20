namespace SoftUni
{

    using System;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = DeleteProjectById(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string DeleteProjectById(SoftUniContext softUniContext)
        {
            int projectToRemoveId = 3;

            Project projectToRemove = softUniContext.Projects
                                            .FirstOrDefault(p => p.ProjectId == projectToRemoveId);

            EmployeeProject[] employeeProjectsToRemove = softUniContext.EmployeesProjects
                                            .Where(p => p.ProjectId == projectToRemoveId)
                                            .ToArray();

            softUniContext.EmployeesProjects.RemoveRange(employeeProjectsToRemove);
            softUniContext.Projects.Remove(projectToRemove);
            softUniContext.SaveChanges();

            var result = new StringBuilder();
            var projects = softUniContext.Projects
                                                   .Take(10)
                                                   .Select(p => p.Name)
                                                   .ToArray();
            foreach (var project in projects)
            {
                result.AppendLine($"{project}");            
            }

            return result.ToString().TrimEnd();
        }
    }
}
