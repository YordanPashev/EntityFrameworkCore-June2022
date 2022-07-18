namespace MiniORM.App
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;

    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniDbContext softUniDbContext = new SoftUniDbContext(Config.ConnectionString);

            Employee[] employedEmployees = softUniDbContext
                .Employees
                .ToArray();

            foreach (Employee e in employedEmployees)
            {
                string isEmployed = e.IsEmployed
                                    ? "Employed"
                                    : "No longer with the company";

                Console.WriteLine($"{e.Id}. {e.FirstName} {e.LastName}. Status: {isEmployed}.");
            }

            //Enable - Migrations

            //Console.WriteLine("Trying to add an Employee...");

            //Employee newEmployee = new Employee()
            //{
            //    FirstName = "Bako Ivan",
            //    LastName = "Petrov",
            //    IsEmployed = false,
            //    DepartmentId = 1
            //};
            //softUniDbContext.Employees.Add(newEmployee);
            //softUniDbContext.SaveChanges();

            //Console.WriteLine("Success!");

            //    Console.WriteLine("Success!");

            //    Console.WriteLine("Trying to update an Employee...");
            //    Employee update = softUniDbContext
            //        .Employees
            //        .First(e => e.FirstName == "Peter");
            //    update.IsEmployed = true;
            //    softUniDbContext.SaveChanges();

            //    Console.WriteLine("Success!");
            //}
        }
    }
}
