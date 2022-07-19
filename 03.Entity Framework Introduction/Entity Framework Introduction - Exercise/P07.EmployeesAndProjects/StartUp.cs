﻿namespace SoftUni
{

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = GetEmployeesInPeriod(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetEmployeesInPeriod(SoftUniContext softUniContext)
        {
            var employees = softUniContext.Employees
                .Where(e => e.EmployeesProjects
                    .Any(p => p.Project.StartDate.Year >= 2001
                              && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                        .Select(p => p.Project)
                })
                .Take(10)
                .ToList();

            var result = new StringBuilder();

            foreach (var emp in employees)
            {
                var employeeName = emp.FirstName + " " + emp.LastName;
                var managerName = emp.ManagerFirstName + " " + emp.ManagerLastName;

                result.AppendLine($"{employeeName} - Manager: {managerName}");

                foreach (var project in emp.Projects)
                {
                    var projectName = project.Name;
                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var endDate = project.EndDate == null
                        ? "not finished"
                        : project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    result.AppendLine($"--{projectName} - {startDate} - {endDate}");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
