using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using EFCoreBestPractices.Core.Models;
using EFCoreBestPractices.Core.Services;
using EFCoreBestPractices.Core.Contracts;
using EFCoreBestPractices.Infrastructure.Data;
using EFCoreBestPractices.Infrastructure.Data.Common;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddUserSecrets("5d525de6-1afd-46d9-a3a2-ff6acd0db765")
    .Build();

var serviceProvider = new ServiceCollection()
    .AddDbContext<SoftUniContext>(options => 
    {
        options.UseSqlServer(configuration.GetConnectionString("SoftUniConnection"));
    })
    .AddScoped<ISoftUniRepository, SoftUniRepository>()
    .AddScoped<IEmployeeService, EmployeeService>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var service = scope.ServiceProvider.GetService<IEmployeeService>();
var employees = await service.GetEmployeesFromDepartment(7);

ImportDepartmentModel department = new ImportDepartmentModel() 
{ 
    Name = "QA",
    ManagerId = 2
};

string addResult = await service.AddDepartment(department);

Console.WriteLine(addResult);


foreach (var employee in employees)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName}, {employee.JobTitle}, {employee.Salary}");
}
