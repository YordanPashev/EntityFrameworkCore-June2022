namespace CarDealer
{

    using System.IO;

    using CarDealer.Models;
    using CarDealer.Data;
    using CarDealer.DTO.Car;
    using CarDealer.DTO;

    using AutoMapper;
    using Newtonsoft.Json;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filepath = ("../../../Datasets/cars.json");
            string json = File.ReadAllText(filepath);

            string result = ImportCars(context, json);
            System.Console.WriteLine(result);
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            ImportCarDTO[] carsDTO = JsonConvert.DeserializeObject<ImportCarDTO[]>(inputJson);

            Car[] cars = new Car[carsDTO.Length];

            for (int i = 0; i < carsDTO.Length; i++)
            {
                Car carToAdd = new Car()
                {
                    Model = carsDTO[i].Model,
                    Make = carsDTO[i].Make,
                    TravelledDistance = carsDTO[i].TravelledDistance
                };

                foreach (int partIdDTO in carsDTO[i].PartsId.Distinct())
                {
                    carToAdd.PartCars.Add(new PartCar
                    {
                        PartId = partIdDTO
                    });
                }

                cars[i] = carToAdd;
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            int addingCarsCount = carsDTO.Length;
            return $"Successfully imported {addingCarsCount}.";
        }
    }
}