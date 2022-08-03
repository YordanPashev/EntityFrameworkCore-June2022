namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Data;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using XmlFacade;

    public class StartUp
    {
        static string filePath;

        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();


            filePath = "../../../Datasets/cars.xml";

            string inputXml = File.ReadAllText(filePath);
            string result = ImportCars(context, inputXml);
            Console.WriteLine(result);
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            string rootElementName = "Cars";
            ImportCarDTO[] carsDTO = XMLConverter.Deserializer<ImportCarDTO>(inputXml, rootElementName);

            Car[] cars = carsDTO.Select(cDto => new Car
            {
                Model = cDto.Model,
                Make = cDto.Make,
                TravelledDistance = cDto.TravelledDistance,
                PartCars = cDto.Parts
                             .Select(pc => pc.Id)
                             .Distinct()
                             .Where(pDto => context.Parts.Any(p => pDto == p.Id))
                             .Select(pId => new PartCar
                             {
                                 PartId = pId
                             })
                             .ToArray()
            })
             .ToArray();

            context.Cars.AddRange(cars);
            context.SaveChanges();

            int addedCarsCount = cars.Count();
            return $"Successfully imported {addedCarsCount}";
        }
    }
}
