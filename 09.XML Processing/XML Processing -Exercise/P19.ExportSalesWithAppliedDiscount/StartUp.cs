namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

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
            string filePath = "../../../Results/sales-discounts..xml";
            string xmlStringResult = GetSalesWithAppliedDiscount(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            string rootElementName = "sales";

            ExportSaleWithDiscountInfoDTO[] salesDTO = context.Sales
                .Include(s => s.Car)
                .ThenInclude(c => c.PartCars)
                .ThenInclude(cp => cp.Part)
                .Select(s => new ExportSaleWithDiscountInfoDTO()
                {
                    CarInfo = new ExportCarInfoDTO()
                    {
                        Car = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Discount = (double)s.Discount,
                    CustomerName = s.Customer.Name,
                    SalePriceWithoutDiscount = GetSalePriceWithoutDiscount(s.Car),
                    SalePriceWithIncludedDiscount = GetSalePriceWithIncludedDiscount(s.Car, s.Discount)
                })
                .ToArray();

            string xmlStringResult = XMLConverter.Serialize(salesDTO, rootElementName);

            return xmlStringResult;
        }

        private static decimal GetSalePriceWithoutDiscount(Car car)
           => car.PartCars.Sum(pc => pc.Part.Price);

        private static decimal GetSalePriceWithIncludedDiscount(Car car, decimal discount)
           => discount > 0 ? car.PartCars.Sum(pc => pc.Part.Price) - (car.PartCars.Sum(pc => pc.Part.Price) * (discount / 100))
                           : GetSalePriceWithoutDiscount(car);
    }
}