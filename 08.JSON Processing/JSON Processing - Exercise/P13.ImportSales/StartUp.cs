namespace CarDealer
{

    using System;
    using System.IO;
    using System.Linq;


    using CarDealer.DTO.Sales;
    using CarDealer.Models;
    using CarDealer.Data;

    using AutoMapper;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filepath = ("../../../Datasets/sales.json");

            string json = File.ReadAllText(filepath);

            string result = ImportSales(context, json);
            System.Console.WriteLine(result);
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            ImportSaleDTO[] salesDTO = JsonConvert.DeserializeObject<ImportSaleDTO[]>(inputJson);
            Sale[] sales = Mapper.Map<Sale[]>(salesDTO.Where(s => IsCardIdExist(context, s.CarId) && 
                                                                  IsCustomerExist(context, s.CustomerId)));

            context.Sales.AddRange(sales);
            context.SaveChanges();

            int addedSalesCount = sales.Length;
            return $"Successfully imported {addedSalesCount}.";
        }

        public static bool IsCustomerExist(CarDealerContext context, int customerId)
            => context.Customers.Any(c => c.Id == customerId);

        public static bool IsCardIdExist(CarDealerContext context, int carId)
            => context.Cars.Any(c => c.Id == carId);
    }
}