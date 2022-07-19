namespace SoftUni
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
            string allEmployeesInfo = GetAddressesByTown(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string GetAddressesByTown(SoftUniContext softUniContext)
        {
            var addressesInfo = softUniContext.Addresses
                                                 .Select(a => new
                                                 {
                                                     FullAddress = a.AddressText,
                                                     TownName = a.Town.Name,
                                                     EmployeeCount = a.Employees.Count
                                                 })
                                                 .OrderByDescending(a => a.EmployeeCount)
                                                 .ThenBy(a => a.TownName)
                                                 .ThenBy(a => a.FullAddress)
                                                 .Take(10)
                                                 .ToList();

            StringBuilder result = new StringBuilder();

            foreach (var address in addressesInfo)
            {
                result.AppendLine($"{address.FullAddress}, {address.TownName} - {address.EmployeeCount} employees");
            }

            return result.ToString().TrimEnd();
        }
    }
}
