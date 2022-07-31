namespace CarDealer
{
    using System;
    using System.Linq;
    using System.IO;
    using CarDealer.Models;
    using CarDealer.Data;

    using AutoMapper;
    using Newtonsoft.Json;
    using CarDealer.DTO.Sales;
    using CarDealer.DTO.Gustomer;
    using AutoMapper.QueryableExtensions;
    using System.Globalization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/ordered-customers.json");


            string jsonResult = GetOrderedCustomers(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            ExportCustomerDTO[] orderedCustomers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new ExportCustomerDTO
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    IsYoungDriver = c.IsYoungDriver,
                })
                .ToArray();

            string jsonResult = JsonConvert.SerializeObject(orderedCustomers, Formatting.Indented);

            return jsonResult;
        }
    }
}