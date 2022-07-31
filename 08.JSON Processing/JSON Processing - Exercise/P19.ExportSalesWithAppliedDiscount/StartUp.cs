namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using CarDealer.Data;
    using CarDealer.DTO.Car;
    using CarDealer.DTO.Sales;

    using AutoMapper;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/sales-discounts.json");


            string jsonResult = GetSalesWithAppliedDiscount(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            ExportSaleDiscountInfoDTO[] topTenSales = context
                                .Sales
                                .Include(s => s.Customer)
                                .Include(s => s.Car)
                                .ThenInclude(c => c.PartCars)
                                .Select(s => new ExportSaleDiscountInfoDTO
                                {
                                    Car = new ExportCarShortInfoDTO
                                    { 
                                        Make = s.Car.Make,
                                        Model = s.Car.Model,
                                        TravelledDistance = s.Car.TravelledDistance
                                    },
                                    CustomerName = s.Customer.Name,
                                    Discount = s.Discount.ToString("F2"),
                                    Price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("F2"),
                                    PriceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price) - (s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100)).ToString("F2")
                                })
                                .Take(10)
                                .ToArray();

            var json = JsonConvert.SerializeObject(topTenSales, Formatting.Indented);

            return json;
        }
    }
}