namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.Dtos.Export;

    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string filePath = "../../../Results/customers-total-sales.xml";
            string xmlStringResult = GetTotalSalesByCustomer(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            string rootElementName = "customers";

            ExportCustomerSalesDTO[] carsWithDrivers = context.Customers
                .Include(c => c.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(c => c.PartCars)
                .ThenInclude(c => c.Part)
                .Where(c => c.Sales.Any(s => s.Car != null))
                .ToArray()
                .Select(c => new ExportCustomerSalesDTO
                {
                    FullName = c.Name,
                    CarsBought = c.Sales.Count,
                    MoneySpent = CalculateCustomerTotalMoneySpent(c.Sales.Select(c => c.Car))
                })
                .OrderByDescending(c => c.MoneySpent)
                .ToArray();

            string xmlStringResult = XMLConverter.Serialize(carsWithDrivers, rootElementName);

            return xmlStringResult;
        }

        private static decimal CalculateCustomerTotalMoneySpent(IEnumerable<Car> cars)
            => Math.Round(cars.Sum(c => c.PartCars.Sum(pc => pc.Part.Price)), 2);

        //private static decimal CalculateCustomerTotalMoneySpent(IEnumerable<Car> cars)
        //{
        //    decimal result = 0;

        //    foreach (Car car in cars)
        //    {
        //        result += car.PartCars.Sum(pc => pc.Part.Price);
        //    }

        //    return Math.Round(result, 2);
        //}
    }
}