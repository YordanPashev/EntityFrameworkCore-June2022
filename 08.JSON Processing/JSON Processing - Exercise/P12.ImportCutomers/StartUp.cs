namespace CarDealer
{

    using System.IO;

    using CarDealer.Models;
    using CarDealer.Data;
    using CarDealer.DTO.Gustomer;

    using AutoMapper;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            string filepath = ("../../../Datasets/customers.json");
            string json = File.ReadAllText(filepath);

            string result = ImportCustomers(context, json);
            System.Console.WriteLine(result);
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            ImportCustomerDTO[] customersDTo = JsonConvert.DeserializeObject<ImportCustomerDTO[]>(inputJson);
            Customer[] customers = Mapper.Map<Customer[]>(customersDTo);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            int addedCustomersCount = customers.Length;
            return $"Successfully imported {addedCustomersCount}.";
        }
    }
}