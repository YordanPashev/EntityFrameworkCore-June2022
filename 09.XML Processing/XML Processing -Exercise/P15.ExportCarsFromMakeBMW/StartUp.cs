namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using XmlFacade;
    using CarDealer.Data;
    using CarDealer.Dtos.Export;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string filePath = "../../../Results/bmw-cars.xml";
            string xmlStringResult = GetCarsFromMakeBmw(context);

            File.WriteAllText(filePath, xmlStringResult);
            Console.WriteLine(xmlStringResult);
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context) 
        { 

            string rootElementName = "cars";

            ExportCarFromMakeBmwDto[] carsWithDrivers = context.Cars
                   .Where(c => c.Make == "BMW")
                   .OrderBy(c => c.Model)
                   .ThenByDescending(c => c.TravelledDistance)
                   .Select(c => new ExportCarFromMakeBmwDto
                   {
                       Id = c.Id,
                       Model = c.Model,
                       TravelledDistance = c.TravelledDistance
                   })
                   .ToArray();

            string xmlStringResult = XMLConverter.Serialize(carsWithDrivers, rootElementName);

            return xmlStringResult;
        }
    }
}