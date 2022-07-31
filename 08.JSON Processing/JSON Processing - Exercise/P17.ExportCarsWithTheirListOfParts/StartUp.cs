namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using CarDealer.Data;
    using CarDealer.DTO.Car;
    using CarDealer.DTO.Part;

    using AutoMapper;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/cars-and-parts.json");


            string jsonResult = GetCarsWithTheirListOfParts(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            ExportCarFullInfoDTO[] carsPartsResult = context.Cars
                .Include(c => c.PartCars)
                .ThenInclude(cp => cp.Part)
                .Select(c => new ExportCarFullInfoDTO
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(pc => new ExportPartNameAndPriceDTO
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price.ToString("F2"),
                    })
                })
                .ToArray();


            List<ExportCarFinalResultDTO> finalResult = new List<ExportCarFinalResultDTO>();

            foreach (ExportCarFullInfoDTO carParts in carsPartsResult)
            {
                ExportCarShortInfoDTO carShortInfo = new ExportCarShortInfoDTO()
                {
                    Make = carParts.Make,
                    Model = carParts.Model,
                    TravelledDistance = carParts.TravelledDistance,
                };

                finalResult.Add(new ExportCarFinalResultDTO()
                {
                    Car = carShortInfo,
                    Parts = carParts.Parts.ToArray()
                });
            }

            string jsonResult = JsonConvert.SerializeObject(finalResult, Formatting.Indented);

            return jsonResult;
        }
    }
}