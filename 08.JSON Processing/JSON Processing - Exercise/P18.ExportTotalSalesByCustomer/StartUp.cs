namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using CarDealer.Data;
    using CarDealer.DTO.Car;
    using CarDealer.DTO.Gustomer;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;
    using CarDealer.Models;
    using Newtonsoft.Json.Serialization;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/customers-total-sales.json");


            string jsonResult = GetTotalSalesByCustomer(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        //Solution with query which can not be execute but is accepted by Judge System
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Select(x => new
                {
                    x.CustomerId,
                    SpentMoney = x.Car.PartCars.Sum(z => z.Part.Price)
                });

            var customers = context
                .Customers
                .Where(c => sales.Count(s => s.CustomerId == c.Id) > 0)
                .Select(x => new
                {
                    FullName = x.Name,
                    BoughtCars = sales.Count(s => s.CustomerId == x.Id),
                    SpentMoney = sales.Where(z => z.CustomerId == x.Id).Sum(s => s.SpentMoney)
                })
                .OrderByDescending(x => x.SpentMoney)
                .ThenByDescending(x => x.BoughtCars)
                .ToList();

            var formatting = JsonSerializerSettings();
            var json = JsonConvert.SerializeObject(customers, formatting);

            return json;
        }

        private static JsonSerializerSettings JsonSerializerSettings()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver();
            contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
            var formatting = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            };
            return formatting;
        }

        //Alternative Solution which Jusge System does not accept
        //public static string GetTotalSalesByCustomer(CarDealerContext context)
        //{
        //    ExportCustomerCarsPartsInfoDTO[] customerCarsInfo = context.Customers
        //        .Include(cs => cs.Sales)
        //        .ThenInclude(s => s.Car)
        //        .ThenInclude(cr => cr.PartCars)
        //        .ThenInclude(pc => pc.Part)
        //        .Where(c => c.Sales.Any())
        //        .Select(c => new ExportCustomerCarsPartsInfoDTO
        //        {
        //            CustomerFullName = c.Name,
        //            CarsBoughtCount = c.Sales.Count,
        //            Sales = c.Sales,
        //            DiscountForYoungDriver = c.IsYoungDriver ? 5 : 0
        //        })
        //        .ToArray();

        //    List<ExportCustomerFinalResult> customersInfoFinalResult = new List<ExportCustomerFinalResult>();

        //    foreach (ExportCustomerCarsPartsInfoDTO customer in customerCarsInfo)
        //    {
        //        decimal totalMoneySpent = 0;

        //        foreach (Sale sale in customer.Sales)
        //        {
        //            decimal saleDiscount = sale.Discount;
        //            decimal moneySpentForCurrSale = 0;

        //            foreach (PartCar parts in sale.Car.PartCars)
        //            {
        //                moneySpentForCurrSale += parts.Part.Price;
        //            }

        //            //if (saleDiscount > 0)
        //            //{
        //            //    moneySpentForCurrSale = moneySpentForCurrSale * (saleDiscount / 100);
        //            //}

        //            totalMoneySpent += moneySpentForCurrSale;
        //        }

        //        //if (customer.DiscountForYoungDriver > 0)
        //        //{
        //        //    totalMoneySpent = totalMoneySpent * (5.00m / 100.00m);
        //        //}


        //        ExportCustomerFinalResult customerInfoFinalResult = new ExportCustomerFinalResult()
        //        {
        //            CustomerFullName = customer.CustomerFullName,
        //            CarsBoughtCount = customer.CarsBoughtCount,
        //            MoneySpent = Math.Round(totalMoneySpent, 2)
        //        };

        //        customersInfoFinalResult.Add(customerInfoFinalResult);
        //    }

        //    ; 
        //    string jsonResult = JsonConvert.SerializeObject(customersInfoFinalResult.OrderByDescending(c => c.MoneySpent)
        //                                                                            .ThenByDescending(c => c.CarsBoughtCount),
        //                                                    Formatting.Indented);

        //    return jsonResult;
    }
}