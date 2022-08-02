namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string filePath = "../../../Results/cars-and-parts.xml";
            string xmlStringResult = GetCarsWithTheirListOfParts(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context) 
        { 

            string rootElementName = "cars";

            ExportCarsWithListOfPartsDTO[] carsWithDrivers = context.Cars
                .Include(c => c.PartCars)
                .ThenInclude(pc => pc.Part)
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .ToArray()
                .Select(c => new ExportCarsWithListOfPartsDTO
                {
                   Make = c.Make,
                   Model = c.Model,
                   TravalledDistance = c.TravelledDistance,
                   Parts = c.PartCars.Select(pc => new ExportPartNameAndPriceDTO
                   {
                       Name = pc.Part.Name,
                       Price = pc.Part.Price
                   })
                   .OrderByDescending(p => p.Price)
                   .ToArray()
                })
                .Take(5)
                .ToArray(); 

            string xmlStringResult = XMLConverter.Serialize(carsWithDrivers, rootElementName);

            return xmlStringResult;
        }
    }
}