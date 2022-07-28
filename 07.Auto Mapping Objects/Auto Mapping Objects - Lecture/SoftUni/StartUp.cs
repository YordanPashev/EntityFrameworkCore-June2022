using AutoMapper;
using AutoMapper.QueryableExtensions;
using SoftUni;
using SoftUni.Configurations;
using SoftUni.Infrastructure.DTOs;

SoftUniContext context  = new SoftUniContext();

var config = new MapperConfiguration(c =>
{
    c.AddProfile<EmployeeMappingProfile>();
});

var mapper = config.CreateMapper();

//Auto Mapping
EmployeeDTO[] employeesInSelectedDepartment = context.Employees
    .Where(e => e.DepartmentId == 4)
    .ProjectTo<EmployeeDTO>(config)
    .ToArray();

foreach (var employee in employeesInSelectedDepartment)
{
    Console.WriteLine($"{employee.FullName} works in {employee.DepartmentName} as {employee.JobTitle}.");
}


////Manual Mapping
//var employee = context.Employees
//    .Where(e => e.EmployeeId == 1)
//    .Select(e => new EmployeeDTO ()
//    {
//        FirstName = e.FirstName,
//        LastName = e.LastName,
//        JobTitle = e.JobTitle,
//        Department = e.Department
//    })
//    .ToArray();

//Console.WriteLine($"{employee.FullName} works in {employee.DepartmentName} as {employee.JobTitle}.");


