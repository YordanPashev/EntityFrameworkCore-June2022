namespace TeisterMask.DataProcessor
{
    using System;
    using System.Linq;
    using System.Globalization;

    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using XmlFacade;
    using TeisterMask.DataProcessor.ExportDto;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            ExportProjectDto[] projects = context.Projects
                .Include(p => p.Tasks)
                .ToArray()
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectDto
                {
                    ProjectCount = p.Tasks.Count(),
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate.HasValue ? "Yes"
                                                    : "No",
                    Tasks = p.Tasks.Select(t => new ExportTaskDto()
                    {
                        Name = t.Name,
                        Label = t.LabelType.ToString()
                    })
                    .OrderBy(t => t.Name)
                    .ToArray()
                })
                .OrderByDescending(e => e.Tasks.Count())
                .ThenBy(e => e.ProjectName)
                .ToArray();

            string rootElement = "Projects";
            string xmlResult = XMLConverter.Serialize(projects, rootElement);

            return xmlResult.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            ExportEmployeeWithHisTasksDto[] busiestEmployees = context.Employees
                                               .Include(e => e.EmployeesTasks)
                                               .ThenInclude(et => et.Employee)
                                               .Where(e => e.EmployeesTasks.Any(e => e.Task.OpenDate >= date))
                                               .ToArray()
                                               .Select(e => new ExportEmployeeWithHisTasksDto()
                                               {
                                                   Username = e.Username,
                                                   Tasks = e.EmployeesTasks.Where(et => et.Task.OpenDate >= date)
                                                                           .Select(et => new ExportTaskInfoDto
                                                   {
                                                       TaskName = et.Task.Name,
                                                       OpenDate = et.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                                                       DueDate = et.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                                       LabelType = et.Task.LabelType.ToString(),
                                                       ExecutionType = et.Task.ExecutionType.ToString()
                                                   })
                                                   .OrderByDescending(et => et.DueDate)
                                                   .ThenBy(et => et.TaskName)
                                                   .ToArray()
                                               })
                                               .OrderByDescending(e => e.Tasks.Count())
                                               .ThenBy(e => e.Username)
                                               .Take(10)
                                               .ToArray();

            string jsonResult = JsonConvert.SerializeObject(busiestEmployees, Formatting.Indented);

            return jsonResult;
        }
    }
}