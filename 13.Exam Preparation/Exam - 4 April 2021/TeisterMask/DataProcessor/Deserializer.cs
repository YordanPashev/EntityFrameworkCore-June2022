namespace TeisterMask.DataProcessor
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using Data;
    using XmlFacade;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            string rootElement = "Projects";
            ImportProjectDto[] projectsDto = XMLConverter.Deserializer<ImportProjectDto>(xmlString, rootElement);
            List<Project> validProjects = new List<Project>();
            StringBuilder result = new StringBuilder();

            foreach (ImportProjectDto projectDto in projectsDto)
            {
                bool isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate,
                                                            "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                            DateTimeStyles.None, out DateTime projectOpenDate);

                List<Task> validProjectTasks = new List<Task>();

                if (!IsValid(projectDto) || !isProjectOpenDateValid)
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (ImportTasksDto taskDto in projectDto.Tasks)
                {
                    //If task is valid it will be added
                    if (!TryToAddTaskToProject(taskDto, projectDto, projectOpenDate, validProjectTasks))
                    {
                        result.AppendLine(ErrorMessage);
                        continue;
                    }
                }

                DateTime? projectDueDateNullValue = null;
                bool isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate,
                                                                    "dd/MM/yyyy",
                                                                    CultureInfo.InvariantCulture,
                                                                    DateTimeStyles.None, out DateTime projectDueDate);

                Project project = new Project()
                {
                    Name = projectDto.Name,
                    OpenDate = projectOpenDate,
                    DueDate = isProjectDueDateValid ? projectDueDate
                                                    : projectDueDateNullValue,
                    Tasks = validProjectTasks
                };

                validProjects.Add(project);
                result.AppendLine($"Successfully imported project - {project.Name} with {project.Tasks.Count} tasks.");
            }

            context.Projects.AddRange(validProjects);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            ImportEmployeeDto[] employeersDto = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);
            StringBuilder result = new StringBuilder();
            List<Employee> employeesToAddToDb = new List<Employee>();

            foreach (ImportEmployeeDto employeeDto in employeersDto)
            {
                if (!IsValid(employeeDto))
                {
                    result.AppendLine(ErrorMessage);
                    continue;
                }

                List<EmployeeTask> existingTasks = new List<EmployeeTask>();

                foreach (var taskIdDto in employeeDto.Tasks.Distinct())
                {
                    if (context.Tasks.Any(t => t.Id == taskIdDto))
                    {
                        EmployeeTask task = new EmployeeTask()
                        {
                            TaskId = taskIdDto,
                        };
                        existingTasks.Add(task);
                        continue;
                    };

                    result.AppendLine(ErrorMessage);
                }

                Employee employee = new Employee()
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                    EmployeesTasks = existingTasks
                };

                employeesToAddToDb.Add(employee);
                result.AppendLine($"Successfully imported employee - {employee.Username} with {employee.EmployeesTasks.Count} tasks.");
            }

            context.Employees.AddRange(employeesToAddToDb);
            context.SaveChanges();

            return result.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static bool TryToAddTaskToProject(ImportTasksDto taskDto, ImportProjectDto projectDto, DateTime projectOpenDate, List<Task> validProjectTasks)
        {
            if (!IsValid(taskDto))
            {
                return false;
            }

            bool isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate,
                                                               "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                               DateTimeStyles.None, out DateTime projectDueDate);

            bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate,
                                                           "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None, out DateTime taskOpenDate);

            bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate,
                                                   "dd/MM/yyyy", CultureInfo.InvariantCulture,
                                                   DateTimeStyles.None, out DateTime taskDueDate);

            if (!isTaskOpenDateValid ||
                !isTaskDueDateValid ||
                taskOpenDate < projectOpenDate)
            {
                return false;
            }

            if (isProjectDueDateValid && taskDueDate > projectDueDate)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(ExecutionType), taskDto.ExecutionType) ||
                !Enum.IsDefined(typeof(LabelType), taskDto.LabelType))
            {
                return false;
            }

            Task task = new Task
            {
                Name = taskDto.Name,
                OpenDate = taskOpenDate,
                DueDate = taskDueDate,
                ExecutionType = (ExecutionType)taskDto.ExecutionType,
                LabelType = (LabelType)taskDto.LabelType
            };

            validProjectTasks.Add(task);
            return true;
        }
    }
}