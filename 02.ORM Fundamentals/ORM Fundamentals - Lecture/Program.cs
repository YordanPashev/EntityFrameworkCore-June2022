using System;

namespace ORM_Fundamentals
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualBasic;
    using Data;
    using ORM_Fundamentals.Data.Models;

    internal class Program
    {
        static void Main(string[] args)
        {
            using SoftUniDBContext dbContext = new SoftUniDBContext();

            Department it = new Department()
            {
                Name = "IT"
            };

            Department hr = new Department()
            {
                Name = "HR"
            };

            Department pr = new Department()
            {
                Name = "PR"
            };

            dbContext.Departments.Add(it);
            dbContext.Departments.Add(hr);
            dbContext.Departments.Add(pr);

            Employee bakoIvan = new Employee()
            {
                FirstName = "Bako Ivan",
                LastName = "Ivanov",
                JobTitle = "Sinior C# Web Develper",
                Department = it
            };

            Employee IvankaMarinova = new Employee()
            {
                FirstName = "Ivanka",
                LastName = "Marinova",
                JobTitle = "Intern",
                Department = it
            };

            Employee stoychoOtSelo = new Employee()
            {
                FirstName = "Stoycho",
                LastName = "Karaivanov",
                JobTitle = "Head of PR Department",
                Department = pr
            };

            Employee anastasiaHR = new Employee()
            {
                FirstName = "Anastasia",
                LastName = "Kaneva",
                JobTitle = "Recruter",
                Department = hr
            };

            dbContext.Employees.Add(IvankaMarinova);
            dbContext.Employees.Add(stoychoOtSelo);
            dbContext.Employees.Add(anastasiaHR);

            dbContext.SaveChanges();

            Console.WriteLine("You added new data to database.");

            Employee ivanka = dbContext.Employees
                .FirstOrDefault(e => e.FirstName == "Ivanka");
            dbContext.Employees.Remove(ivanka);

            dbContext.SaveChanges();

            var itEmployees = dbContext.Employees
                .Where(e => e.Department.Name == "IT")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle
                })
                .ToList();

            int count = 0;
            foreach (var itEmployee in itEmployees)
            {
                count++;
                Console.WriteLine($"{count}. {itEmployee.FirstName} {itEmployee.LastName} - {itEmployee.JobTitle}");
            }
        }
    }
}
