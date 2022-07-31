namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;

    using CarDealer.Data;
    using CarDealer.DTO.Car;

    using AutoMapper;
    using Newtonsoft.Json;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filePath = ("../../../Results/toyota-cars.json");


            string jsonResult = GetCarsFromMakeToyota(context);

            Console.WriteLine(jsonResult);

            File.WriteAllText(filePath, jsonResult);
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            ExportCarDTO[] toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<ExportCarDTO>()
                .ToArray();

            string jsonResult = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            return jsonResult;
        }
    }
}