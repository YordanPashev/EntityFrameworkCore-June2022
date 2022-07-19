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
            string allEmployeesInfo = AddNewAddressToEmployee(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string AddNewAddressToEmployee(SoftUniContext softUniContext)
        {
            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            Employee employeeWithLastNameNakov = softUniContext.Employees
                                            .FirstOrDefault(e => e.LastName == "Nakov");
            employeeWithLastNameNakov.Address = newAddress;

            softUniContext.SaveChanges();

            var employees = softUniContext.Employees
                                .Select(e => new
                                                {
                                                    e.Address.AddressText,
                                                    e.AddressId
                                                })
                                .OrderByDescending(e => e.AddressId)
                                .Take(10)
                                .ToArray();

            StringBuilder result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.AddressText}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
