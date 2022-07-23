using Microsoft.EntityFrameworkCore;
using SoftUni;

SoftUniContext context = new SoftUniContext();
var resutl = context.Employees
    .Include(e => e.Department)
    .GroupBy(d => new 
    {   DepartmentName = d.Department.Name, 
        DeparmentID = d.DepartmentId
    })
    .Select(grp => new 
    { 
        grp.Key.DepartmentName,
        DepartmentEmployees = grp.Select(de => de.FirstName)
    })
    .ToList();

Console.WriteLine("");
