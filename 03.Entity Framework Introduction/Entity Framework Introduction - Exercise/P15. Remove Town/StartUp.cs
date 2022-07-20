namespace SoftUni
{

    using System;
    using System.Linq;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            string allEmployeesInfo = RemoveTown(softUniContext);
            Console.WriteLine(allEmployeesInfo);
        }

        public static string RemoveTown(SoftUniContext softUniContext)
        {
            Town townToRemove = softUniContext.Towns
                                                .FirstOrDefault(t => t.Name == "Seattle");

            Address[] addressesToRemove = softUniContext.Addresses
                                            .Where(p => p.TownId == townToRemove.TownId)
                                            .ToArray();

            Employee[] employeesToSetNullForAddress = softUniContext.Employees
                                                        .Where(e => e.Address.TownId == townToRemove.TownId)
                                                        .ToArray();

            foreach (var employee in employeesToSetNullForAddress)
            {
                employee.Address = null;
            }

            softUniContext.Addresses.RemoveRange(addressesToRemove);
            softUniContext.Towns.Remove(townToRemove);
            softUniContext.SaveChanges();

            return $"{addressesToRemove.Count()} addresses in Seattle were deleted";
        }
    }
}
