namespace SoftUni
{

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = GetLatestProjects(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetLatestProjects(SoftUniContext softUniContext)
        {
            var lastTenProjectsInfo = softUniContext.Projects
                                                .OrderByDescending(p => p.StartDate)
                                                .Take(10)
                                                .Select(p => new
                                                                 {
                                                                     p.Name,
                                                                     p.Description,
                                                                     p.StartDate
                                                                 })
                                                .OrderBy(p => p.Name);

            StringBuilder result = new StringBuilder();

            foreach (var project in lastTenProjectsInfo)
            {
                string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                result.AppendLine($"{project.Name}");
                result.AppendLine($"{project.Description}");
                result.AppendLine($"{startDate}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
