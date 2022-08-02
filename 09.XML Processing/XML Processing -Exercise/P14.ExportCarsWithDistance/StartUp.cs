namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;

    using AutoMapper;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string filePath = "../../../Results/cars.xml";
            string xmlStringResult = GetCarsWithDistance(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetCarsWithDistance(CarDealerContext context) 
        { 

            string rootElementName = "cars";

            ExportCarWithDistanceDTO[] carsWithDrivers = context.Cars
                                                                .Where(c => c.TravelledDistance > 2_000_000)
                                                                .OrderBy(c => c.Make)
                                                                .ThenBy(c => c.Model)
                                                                .Select(c => new ExportCarWithDistanceDTO
                                                                {
                                                                    Make = c.Make,
                                                                    Model = c.Model,
                                                                    TravelledDistance = c.TravelledDistance,
                                                                })
                                                                .Take(10)
                                                                .ToArray();

            return XMLConverter.Serialize(carsWithDrivers, rootElementName);
        }
    }
}