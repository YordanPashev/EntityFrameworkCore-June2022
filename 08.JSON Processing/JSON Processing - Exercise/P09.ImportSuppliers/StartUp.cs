namespace CarDealer
{

    using System.IO;
    using System.Collections.Generic;

    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.DTO.Supplier;

    using AutoMapper;
    using Newtonsoft.Json;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(CarDealerProfile)));

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Datasets/suppliers.json"); 
            string json = File.ReadAllText(filepath);

            string result = ImportSuppliers(context, json);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            ImportSupplierDTO[] suppliersDTO = JsonConvert.DeserializeObject<ImportSupplierDTO[]>(inputJson);
            Supplier[] suplliers = Mapper.Map<Supplier[]>(suppliersDTO);

            context.Suppliers.AddRange(suplliers);
            context.SaveChanges();

            int addedSuppliersCount = suplliers.Length;
            return $"Successfully imported {addedSuppliersCount}.";
        }
    }
}